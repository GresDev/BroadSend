using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BroadSend.Server.ViewModels
{
    public class PresenterAliasCreateViewModel
    {
        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [Remote ("CheckForItemAliasIsUnique", "Presenter")]
        public string Alias { get; set; }
        public int PresenterId { get; set; }
    }
}
