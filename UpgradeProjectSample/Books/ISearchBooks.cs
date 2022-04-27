using System.Collections.Generic;
using System.Threading.Tasks;
using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books
{
    public interface ISearchBooks
    {
        Task<List<Book>> GetAllAsync();
        Task<List<SearchedBook>> GetAsync(SearchBookCriteria criteria);
    }
}
