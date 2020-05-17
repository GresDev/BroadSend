using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class PresenterController : ComplexItemController<Presenter, PresenterAlias, PresenterCreateViewModel,
        PresenterAliasCreateViewModel>
    {
        public PresenterController(
            IPresenterRepository presenterRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(presenterRepository, sharedLocalizer, userManager)
        {
        }
    }
}