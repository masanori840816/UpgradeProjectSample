using System.Collections.Generic;
using System.Threading.Tasks;
using UpgradeProjectSample.Books.Dto;
using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books
{
    public interface IBookService
    {
        Task CreateSampleAsync(string name);
        Task<List<Book>> GetAllAsync();
        Task<List<SearchedBook>> GetAsync(SearchBookCriteria criteria);

        Task UpdateBookAsync(int id);
        Task UpdateAuthorAsync(int id);
    }
}
