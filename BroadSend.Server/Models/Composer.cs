﻿using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils;

namespace BroadSend.Server.Models
{
    public class Composer : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [ComposerNameIsUnique]
        public string Name { get; set; }
    }
}
