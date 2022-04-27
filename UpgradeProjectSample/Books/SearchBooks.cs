using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            var whereClause = "";
            if(string.IsNullOrEmpty(criteria.Name) == false)
            {
                whereClause = " WHERE b.name LIKE '%" + criteria.Name + "%'";
            }
            if(string.IsNullOrEmpty(criteria.AuthorName) == false)
            {
                if(string.IsNullOrEmpty(whereClause))
                {
                    whereClause = " WHERE ";
                }
                else
                {
                    whereClause += " AND ";
                }
                whereClause += "a.name LIKE '%" + criteria.AuthorName + "%'";
            }
            var query = this.context.SearchedBooks.FromSql(
                "SELECT b.id AS \"BookId\", b.name AS \"BookName\", a.name AS \"AuthorName\" FROM book b INNER JOIN author AS a ON b.author_id = a.id" +
                whereClause +
                " ORDER BY b.id");
            return await query.ToListAsync<SearchedBook>();
        }
    }
}
