using UpgradeProjectSample.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UpgradeProjectSample.Controllers
{
    [Authorize(AuthenticationSchemes = UserTokens.AuthSchemes)]
    public class PageController: Controller
    {
        private readonly ILogger<PageController> logger;
        public PageController(ILogger<PageController> logger)
        {
            this.logger = logger;
        }

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

}
