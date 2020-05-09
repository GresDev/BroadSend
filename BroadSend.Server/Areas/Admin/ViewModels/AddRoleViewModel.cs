using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BroadSend.Server.Areas.Admin.ViewModels
{
    public class AddRoleViewModel
    {
        [Required (ErrorMessage = "Введите наименование роли")]
        [Remote("CheckIfRoleNameIsUnique", "Admin")]
        [StringLength(64, ErrorMessage = "Не более 64-х символов")]
        [Display (Name = "Наименование роли")]
        public string Name { get; set; }
    }
}
