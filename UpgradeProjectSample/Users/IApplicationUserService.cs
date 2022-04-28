using UpgradeProjectSample.Apps;
using UpgradeProjectSample.Users.Dto;

namespace UpgradeProjectSample.Users
{
    public interface IApplicationUserService
    {
        Task<UserActionResult> SigninAsync(SigninValue value, ISession session);
        Task<UserActionResult> CreateSampleAsync();
        Task SignoutAsync();
    }
}