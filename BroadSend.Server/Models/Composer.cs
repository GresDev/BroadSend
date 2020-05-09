using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BroadSend.Server.Models
{
    public class Composer : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [Remote("CheckForItemNameIsUnique", "Composer")]
        public string Name { get; set; }
    }
}
