using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class VendorController : SimpleItemController<Vendor>
    {
        public VendorController(
            IVendorRepository vendorRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(vendorRepository, sharedLocalizer, userManager)
        {
        }
    }
}