using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class PresenterAlias
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        [Remote ("CheckForItemAliasIsUnique", "Presenter")]
        public string Alias { get; set; }

        public int PresenterId { get; set; }
    }
}
