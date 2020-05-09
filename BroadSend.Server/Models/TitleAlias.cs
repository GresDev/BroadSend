using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class TitleAlias
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "AliasRequired")]
        [StringLength(64, ErrorMessage = "AliasLength", MinimumLength = 1)]
        public string Alias { get; set; }
        public Title Title  { get; set; }
        public int TitleId { get; set; }
    }
}
