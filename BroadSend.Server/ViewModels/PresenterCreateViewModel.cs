using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BroadSend.Server.ViewModels
{
    public class PresenterCreateViewModel
    {

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(256, ErrorMessage = "NameLength", MinimumLength = 1)]
        [Remote("CheckForItemNameIsUnique", "Presenter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [Remote("CheckForItemAliasIsUnique", "Presenter")]
        public string Alias { get; set; }
    }
}
