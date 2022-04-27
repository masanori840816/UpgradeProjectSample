using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace UpgradeProjectSample.Books
{
    public class SearchBooks: ISearchBooks
    {
        private readonly ILogger<SearchBooks> logger;
        private readonly SampleContext context;

        public SearchBooks(ILogger<SearchBooks> logger,
            SampleContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await this.context.Books
                .Include(b => b.Author)
                .Include(b => b.Language)
                .ToListAsync();
        }
        public async Task<List<SearchedBook>> GetAsync(SearchBookCriteria criteria)
        {
            // this.context.SearchedBooks.FromSql から変更.
            var query = this.context.SearchedBooks
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
            var languages = await this.context.Languages
                .Where(L => languageIds.Contains(L.Id))
                .ToListAsync();
            if(languages.Count <= 0)
            {
                return new List<Book>();
            }
            return await this.context.Books
                .Where(b => languages.Any(L => L.Id == b.LanguageId))
                .ToListAsync();
        }
    }
}
