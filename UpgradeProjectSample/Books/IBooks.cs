using System.Threading.Tasks;
using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books
{
    public interface IBooks
    {
        Task CreateAsync(Author author, Book newItem);
        Task<Book> GetTrackedBookAsync(int id);
    }
}
