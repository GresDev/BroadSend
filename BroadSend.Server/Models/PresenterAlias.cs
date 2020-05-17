using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models
{
    public class PresenterAlias : IComplexItemAlias
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string Alias { get; set; }

        public int ParentId { get; set; }
    }
}