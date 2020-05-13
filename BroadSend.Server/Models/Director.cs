using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils;

namespace BroadSend.Server.Models
{
    public class Director : ISimpleItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [DirectorAliasIsUnique]
        public string Alias { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(256, ErrorMessage = "NameLength", MinimumLength = 1)]
        [DirectorNameIsUnique]
        public string Name { get; set; }

    }
}
