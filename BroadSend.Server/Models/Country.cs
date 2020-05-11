using System.ComponentModel.DataAnnotations;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.Models
{
    public class Country : ISimpleItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(256, ErrorMessage = "NameLength"), MinLength(1)]
        public string Name { get; set; }
    }
}