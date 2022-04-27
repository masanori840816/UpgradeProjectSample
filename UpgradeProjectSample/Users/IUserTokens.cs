using System.Collections.Generic;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Users
{
    public interface IUserTokens
    {
        string GenerateToken(ApplicationUser user);
    }
}
