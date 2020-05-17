using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.ViewModels
{
    public class PresenterAliasCreateViewModel : IComplexItemAliasCreateViewModel
    {
        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [PresenterAliasIsUnique]
        public string Alias { get; set; }

        public int ParentId { get; set; }
    }
}