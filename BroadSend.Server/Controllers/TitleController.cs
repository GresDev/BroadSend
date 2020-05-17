using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class
        TitleController : ComplexItemController<Title, TitleAlias, TitleCreateViewModel, TitleAliasCreateViewModel>
    {
        public TitleController(
            ITitleRepository titleRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(titleRepository, sharedLocalizer, userManager)
        {
        }
    }
}