using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UpgradeProjectSample.Apps;
using UpgradeProjectSample.Users.Dto;
using UpgradeProjectSample.Users.Models;
using UpgradeProjectSample.Users.Repositories;

namespace UpgradeProjectSample.Users
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ILogger<ApplicationUserService> logger;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUsers users;
        private readonly IUserTokens userTokens;

        public ApplicationUserService(ILogger<ApplicationUserService> logger,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUsers users,
            IUserTokens userTokens)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.users = users;
            this.userTokens = userTokens;
        }
        public async Task<UserActionResult> SigninAsync(SigninValue value, ISession session)
        {
            var target = await this.users.GetByEmailForSigninAsync(value.Email);
            if(target == null)
            {
                logger.LogError($"e-mail was not registered value: {value.Email}");
                return ActionResultFactory.GetFailed("Invalid e-mail or password");
            }
            var result = await this.signInManager.PasswordSignInAsync(target, value.Password, false, false);
            if(result.Succeeded)
            {
                var token = this.userTokens.GenerateToken(target);
                session.SetString("user-token", token);
                return ActionResultFactory.GetSucceeded();
            }
            return ActionResultFactory.GetFailed("Invalid e-mail or password");
        }
        public async Task<UserActionResult> CreateSampleAsync()
        {
            var newUser = ApplicationUser.CreateSample("example", "example@mail.com", "example");
            var result = await signInManager.UserManager.CreateAsync(newUser);
            if(result.Succeeded)
            {
                return ActionResultFactory.GetSucceeded();
            }
            return ActionResultFactory.GetFailed("Failed creating sample");
        }
        public async Task SignoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
