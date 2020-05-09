using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Areas.Admin.ViewModels
{
    public class EditRoleViewModel
    {
        [Required(ErrorMessage = "Введите наименование роли")]
        [Remote("CheckIfRoleNameIsUnique", "Admin")]
        [StringLength(64, ErrorMessage = "Не более 64-х символов")]
        [Display(Name = "Наименование роли")]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
