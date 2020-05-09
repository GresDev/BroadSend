using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BroadSend.Server.ViewModels
{
    public class PresenterEditViewModel
    {

        public Presenter Presenter { get; set; }

        [Required(ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [Remote("CheckForItemAliasIsUnique", "Presenter")]
        public string PresenterAlias { get; set; }
    }
}
