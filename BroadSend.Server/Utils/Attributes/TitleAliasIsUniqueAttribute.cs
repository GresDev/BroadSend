using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils.Attributes
{
    public class TitleAliasIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var localizer =
                (IStringLocalizer<SharedResource>)validationContext.GetService(
                    typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

            var titleAlias = context?.TitleAliases.AsNoTracking().SingleOrDefault(t => t.Alias == value as string);


            return titleAlias != null
                ? new ValidationResult(localizer?["ErrorDuplicateRecord"])
                : ValidationResult.Success;
        }
    }
}