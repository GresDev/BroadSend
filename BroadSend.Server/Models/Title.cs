using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Title
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "NameRequired")]
        [StringLength(64, ErrorMessage = "NameLength", MinimumLength = 1)]
        public string Name { get; set; }

        [Required (ErrorMessage = "AnonsRequired")]
        [StringLength(2048, ErrorMessage = "AnonsLength", MinimumLength = 1)]
        public string Anons { get; set; }

        public ICollection<TitleAlias> TitleAliases { get; set; }
    }
}
