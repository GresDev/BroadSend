using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BroadSend.Server.Areas.Admin.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(512, ErrorMessage = "NameLength", MinimumLength = 1)]
        //[Remote("CheckIfUserNameIsUnique", "Admin")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        //[Remote("CheckIfEmailIsUnique", "Admin")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "EmailIncorrectFormat")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "PasswordConfirmationRequired")]
        [Compare("Password", ErrorMessage = "PasswordsNotEqual")]
        [DataType(DataType.Password)]
        public string PasswordToConfirm { get; set; }

        //[Display(Name = "Роль  ")]
        public IQueryable<IdentityRole> Roles { get; set; }

        public string SelectedRole { get; set; }
    }
}
