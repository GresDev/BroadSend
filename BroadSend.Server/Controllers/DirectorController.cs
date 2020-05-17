using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class DirectorController : SimpleItemController<Director>
    {
        public DirectorController(
            IDirectorRepository directorRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(directorRepository, sharedLocalizer, userManager)
        {
        }
    }
}