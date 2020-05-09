using BroadSend.Server.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Vendor : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [Remote("CheckForItemNameIsUnique", "Vendor")]
        public string Name { get; set; }

    }
}
