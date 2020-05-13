using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.ViewModels
{
    public class PresenterAliasCreateViewModel
    {
        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string Alias { get; set; }
        public int PresenterId { get; set; }
    }
}
