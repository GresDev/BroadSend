﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using BroadSend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Utils.Attributes
{
    public class ComposerNameIsUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var localizer =
                (IStringLocalizer<SharedResource>)validationContext.GetService(
                    typeof(IStringLocalizer<SharedResource>));

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

            var composer = context?.Composers.AsNoTracking().SingleOrDefault(c => c.Name == value as string);


            return composer != null
                ? new ValidationResult(localizer?["ErrorDuplicateRecord"])
                : ValidationResult.Success;
        }
    }
}