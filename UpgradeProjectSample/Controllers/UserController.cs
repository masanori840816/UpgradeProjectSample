using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UpgradeProjectSample.Apps;
using UpgradeProjectSample.Users;
using UpgradeProjectSample.Users.Dto;

namespace UpgradeProjectSample.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IApplicationUserService userService;
        public UserController(ILogger<UserController> logger,
            IApplicationUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/user/signin")]
        public async Task<UserActionResult> Signin([FromBody] SigninValue value)
        {
            if (value == null)
            {
                return ActionResultFactory.GetFailed("Failed getting sign in values");
            }
            return await this.userService.SigninAsync(value, HttpContext.Session);
        }
        [AllowAnonymous]
        [Route("/user/sample")]
        public async Task<UserActionResult> CreateSample()
        {
            return await this.userService.CreateSampleAsync();
        }
    }
}