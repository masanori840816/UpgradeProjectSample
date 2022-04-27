using System.Threading.Tasks;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Users.Repositories
{
    public interface IApplicationUsers
    {
        Task<ApplicationUser> GetByEmailForSigninAsync(string email);
    }
}
