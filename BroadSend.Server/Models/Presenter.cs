using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Presenter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        //[Remote("CheckForItemNameIsUnique", "Presenter")]
        public string Name { get; set; }

        public List<PresenterAlias> Aliases { get; set; }
    }
}