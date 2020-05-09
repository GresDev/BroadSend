using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.ViewModels
{
    public class TitleAliasCreateViewModel
    {
        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string titleAlias { get; set; }
        public int TitleId { get; set; }
    }
}
