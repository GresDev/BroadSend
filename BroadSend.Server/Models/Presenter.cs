﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.Models
{
    public class Presenter : IComplexItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [PresenterNameIsUnique]
        public string Name { get; set; }

        public IEnumerable<PresenterAlias> PresenterAliases { get; set; }
    }
}