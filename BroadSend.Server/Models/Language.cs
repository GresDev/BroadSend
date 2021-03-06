﻿using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.Models
{
    public class Language : ISimpleItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(64, ErrorMessage = "NameLength", MinimumLength = 1)]
        [LanguageNameIsUnique]
        public string Name { get; set; }
    }
}