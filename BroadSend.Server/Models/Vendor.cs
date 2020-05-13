using BroadSend.Server.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Utils;

namespace BroadSend.Server.Models
{
    public class Vendor : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [VendorNameIsUnique]
        public string Name { get; set; }

    }
}
