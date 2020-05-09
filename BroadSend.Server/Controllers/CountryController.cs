using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Controllers
{
    [Authorize]
    public class CountryController : SimpleItemController<Country>
    {
        public CountryController(
            ICountryRepository countryRepository,
            IStringLocalizer<SharedResource> sharedLocalizer,
            UserManager<IdentityUser> userManager) :
            base(countryRepository, sharedLocalizer, userManager)
        {
        }
    }
}