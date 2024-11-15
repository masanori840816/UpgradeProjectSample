using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;

namespace UpgradeProjectSample.Books
{
    public class SearchBooks(SampleContext context): ISearchBooks
    {
        public async Task<List<Book>> GetAllAsync() => await context.Books
                .Include(b => b.Author)
                .Include(b => b.Language)
                .ToListAsync();
        public async Task<List<SearchedBook>> GetAsync(SearchBookCriteria criteria)
        {
            // this.context.SearchedBooks.FromSql から変更.
            var query = context.SearchedBooks
                .FromSqlRaw("SELECT b.id AS \"BookId\", b.language_id AS \"LanguageId\", b.name AS \"BookName\", a.name AS \"AuthorName\" FROM book b INNER JOIN author AS a ON b.author_id = a.id");

            if(string.IsNullOrEmpty(criteria.Name) == false)
            {
                query = query.Where(b => b.BookName.Contains(criteria.Name));
            }
            if(criteria.LanguageIds.Length > 0)
            {
                query = query.Where(b => criteria.LanguageIds.Contains(b.LanguageId));
            }
            if(string.IsNullOrEmpty(criteria.AuthorName) == false)
            {
                query = query.Where(b => b.AuthorName.Contains(criteria.AuthorName));
            }
            return await query
                .OrderBy(b => b.BookId)
                .ToListAsync();
        }
        public async Task<List<Book>> GetByLanguageIdsAsync(int[] languageIds)
        {
            var languages = await context.Languages
                .Where(L => languageIds.Contains(L.Id))
                .ToListAsync();
            if(languages.Count <= 0)
            {
                return [];
            }
            return await context.Books
                .Where(b => languageIds.Contains(b.LanguageId))
                .ToListAsync();
        }
    }
}
