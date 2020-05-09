using BroadSend.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.ViewModels
{
    public class TitleCreateViewModel
    {
        public Title Title { get; set; }

        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string TitleAlias { get; set; }
    }
}
