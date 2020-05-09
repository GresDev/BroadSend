using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Language : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(64, ErrorMessage = "NameLength", MinimumLength = 1)]
        [Remote("CheckForItemNameIsUnique", "Language")]
        public string Name { get; set; }
    }
}
