using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Areas.Admin.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set;  }

        public string Email { get; set;  }

        [Required(ErrorMessage = "PasswordRequired")]
        //[Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessage = "PasswordsNotEqual")]
        //[Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        public string PasswordToConfirm { get; set; }
    }
}
