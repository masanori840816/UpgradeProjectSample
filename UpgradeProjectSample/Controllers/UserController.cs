using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpgradeProjectSample.Apps;
using UpgradeProjectSample.Users;
using UpgradeProjectSample.Users.Dto;

namespace UpgradeProjectSample.Controllers;
public class UserController(IApplicationUserService userService) : Controller
{
    [AllowAnonymous]
    [HttpPost]
    [Route("/user/signin")]
    public async Task<UserActionResult> Signin([FromBody] SigninValue value)
    {
        if (value == null)
        {
            return ActionResultFactory.GetFailed("Failed getting sign in values");
        }
        return await userService.SigninAsync(value, HttpContext.Session);
    }
    [AllowAnonymous]
    [Route("/user/sample")]
    public async Task<UserActionResult> CreateSample()
    {
        return await userService.CreateSampleAsync();
    }
}