using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.ViewModels
{
    public class TitleAliasCreateViewModel
    {
        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [TitleAliasIsUnique]
        public string Alias { get; set; }
        public int TitleId { get; set; }
    }
}
