using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils.Attributes
{
    public class LanguageNameIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var localizer = (IStringLocalizer<SharedResource>)validationContext.GetService(typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var language = context?.Languages.AsNoTracking().SingleOrDefault(l => l.Name == value as string);


            return language != null ? new ValidationResult(localizer["ErrorDuplicateRecord"]) : ValidationResult.Success;
        }
    }
}