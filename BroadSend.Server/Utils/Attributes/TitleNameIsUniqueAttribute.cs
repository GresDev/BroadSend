using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils.Attributes
{
    public class TitleNameIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var localizer =
                (IStringLocalizer<SharedResource>)validationContext.GetService(
                    typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

            var title = context?.Titles.AsNoTracking().SingleOrDefault(t => t.Name == value as string);


            return title != null ? new ValidationResult(localizer?["ErrorDuplicateRecord"]) : ValidationResult.Success;
        }
    }
}