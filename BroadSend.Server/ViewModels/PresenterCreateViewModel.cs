using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Utils;

namespace BroadSend.Server.ViewModels
{
    public class PresenterCreateViewModel
    {

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(256, ErrorMessage = "NameLength", MinimumLength = 1)]
        [PresenterNameIsUnique]
        public string Name { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [PresenterAliasIsUnique]
        public string Alias { get; set; }
    }
}
