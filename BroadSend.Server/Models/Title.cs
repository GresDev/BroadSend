using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models
{
    public class Title : IComplexItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "AnonsRequired")]
        [StringLength(4096, ErrorMessage = "AnonsLength", MinimumLength = 1)]
        public string Anons { get; set; }
    }
}