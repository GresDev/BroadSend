using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils
{
    public class PresenterAliasIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var localizer = (IStringLocalizer<SharedResource>)validationContext.GetService(typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var presenterAlias = context?.PresenterAliases.AsNoTracking().SingleOrDefault(p => p.Alias == value as string);


            return presenterAlias != null ? new ValidationResult(localizer["ErrorDuplicateRecord"]) : ValidationResult.Success;
        }
    }
}