using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;

namespace BroadSend.Server.Controllers
{
    public class ErrorController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        private IStringLocalizer<SharedResource> _sharedLocalizer;

        public ErrorController(UserManager<IdentityUser> userManager, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _sharedLocalizer = sharedLocalizer;
            ;
        }

        [Route("ErrorPage92-4596-24956825862-54986-34687875-356-35867-8576")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Log.Error($"User: {_userManager.GetUserName(User)} | Unhandled exception occured");
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorHttpResponse404"];
                    break;
                default:
                    ViewBag.ErrorMessage = _sharedLocalizer["ErrorHttpResponse"] + statusCode;
                    break;
            }
            return View("_HttpResponse");
        }
    }
}