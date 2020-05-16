using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models;
using BroadSend.Server.Utils.Attributes;

namespace BroadSend.Server.ViewModels
{
    public class TitleCreateViewModel
    {
        public Title Title { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        [TitleNameIsUnique]
        public string Name { get; set; }

        [Required(ErrorMessage = "AnonsRequired")]
        [StringLength(4096, ErrorMessage = "AnonsLength", MinimumLength = 1)]
        public string Anons { get; set; }



        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [TitleAliasIsUnique]
        public string Alias { get; set; }
    }
}
