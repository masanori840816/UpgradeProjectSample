using UpgradeProjectSample.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpgradeProjectSample.Controllers;

[Authorize(AuthenticationSchemes = UserTokens.AuthSchemes)]
public class PageController: Controller
{
    [Route("/")]
    [Route("/pages")]
    [Route("/pages/index")]
    public IActionResult Index()
    {
        return View("Views/Index.cshtml");
    }
    [AllowAnonymous]
    [Route("/pages/signin")]
    public IActionResult Signin()
    {
        return View("Views/Signin.cshtml");
    }
}