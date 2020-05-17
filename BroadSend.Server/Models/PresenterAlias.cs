using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.Models
{
    public class PresenterAlias : IComplexItemAlias
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [PresenterAliasIsUnique]
        public string Alias { get; set; }

        public int ParentId { get; set; }
    }
}