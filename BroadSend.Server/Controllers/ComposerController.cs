using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class ComposerController : SimpleItemController<Composer>
    {
        public ComposerController(
            IComposerRepository composerRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(composerRepository, sharedLocalizer, userManager)
        {
        }
    }
}