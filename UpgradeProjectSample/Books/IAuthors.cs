using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books;

public interface IAuthors
{
    Task<Author> GetOrCreateAsync(string name);
    Task<List<Author>> GetByNameAsync(string name);
    Task<Author?> GetTrackedAuthorAsync(int id);
}
