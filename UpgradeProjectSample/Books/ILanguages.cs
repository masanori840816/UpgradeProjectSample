using System.Collections.Generic;
using System.Threading.Tasks;
using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books
{
    public interface ILanguages
    {
        Task<List<Language>> GetAllAsync();
    }
}
