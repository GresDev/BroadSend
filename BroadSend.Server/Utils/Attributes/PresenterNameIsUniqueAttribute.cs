using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils.Attributes
{
    public class PresenterNameIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var localizer =
                (IStringLocalizer<SharedResource>)validationContext.GetService(
                    typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

            var presenter = context?.Presenters.AsNoTracking().SingleOrDefault(p => p.Name == value as string);


            return presenter != null
                ? new ValidationResult(localizer?["ErrorDuplicateRecord"])
                : ValidationResult.Success;
        }
    }
}