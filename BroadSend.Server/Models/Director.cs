using BroadSend.Server.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Director : ISimpleItem
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string Alias { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(256, ErrorMessage = "NameLength", MinimumLength = 1)]
        public string Name { get; set; }

    }
}
