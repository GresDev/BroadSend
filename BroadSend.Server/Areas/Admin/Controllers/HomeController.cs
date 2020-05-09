using System.Linq;
using BroadSend.Server.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BroadSend.Server.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area( "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sharedLocalizer = sharedLocalizer;
        }

        public IActionResult Index()
        {
            return RedirectToAction("UserManagement");
        }

        public IActionResult UserManagement()
        {
            return View(_userManager.Users.OrderBy(u => u.UserName));
        }

        public IActionResult AddUser()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel
            {
                Roles = _roleManager.Roles,
            };

            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = addUserViewModel.UserName,
                    Email = addUserViewModel.Email
                };


                IdentityResult result = _userManager.CreateAsync(user, addUserViewModel.Password).Result;


                if (result.Succeeded)
                {
                    IdentityResult addingRoleResult =
                        _userManager.AddToRoleAsync(user, addUserViewModel.SelectedRole).Result;

                    if (addingRoleResult.Succeeded)
                    {
                        return RedirectToAction("UserManagement", _userManager.Users);
                    }
                }
                else
                {
                    foreach (var resultCode in result.Errors)
                    {
                        if (resultCode.Code.Contains("Password"))
                        {
                            ModelState.AddModelError("Password", resultCode.Description);
                        }

                        if (resultCode.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("UserName", _sharedLocalizer["ErrorDuplicateRecord"]);
                        }

                        if (resultCode.Code == "DuplicateEmail")
                        {
                            ModelState.AddModelError("Email", _sharedLocalizer["ErrorDuplicateRecord"]);
                        }
                    }

                }
            }

            addUserViewModel.Roles = _roleManager.Roles;
            return View(addUserViewModel);
        }

        public IActionResult DeleteUser(string userId)
        {
            IdentityUser user = _userManager.FindByIdAsync(userId).Result;

            int adminCount = 0;

            foreach (var identityUser in _userManager.Users)
            {
                if (_userManager.IsInRoleAsync(identityUser, "Administrator").Result)
                {
                    adminCount++;
                }
            }

            if (_userManager.IsInRoleAsync(user, "Administrator").Result && adminCount == 1)
            {
                ViewBag.ErrorMessage = _sharedLocalizer["ErrorCantDeleteUniqueAdmin"];
                ViewBag.Error = true;
            }
            else
            {
                ViewBag.ErrorMessage = string.Empty;
                ViewBag.Error = false;
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteUser(IdentityUser userToDelete)
        {
            IdentityUser user = _userManager.FindByIdAsync(userToDelete.Id).Result;

            if (user != null)
            {
                IdentityResult result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement", _userManager.Users);
                }
                else
                {
                    ModelState.AddModelError("", _sharedLocalizer["ErrorDbUpdate"]);
                    return View(userToDelete);
                }
            }
            else
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }
        }

        public IActionResult EditUser(string userId)
        {
            IdentityUser user = _userManager.FindByIdAsync(userId).Result;
            EditUserViewModel editUserViewModel = new EditUserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id,
                Roles = _roleManager.Roles,
            };
            editUserViewModel.CurrentRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

            int adminCount = 0;

            foreach (var identityUser in _userManager.Users)
            {
                if (_userManager.IsInRoleAsync(identityUser, "Administrator").Result)
                {
                    adminCount++;
                }
            }

            if (_userManager.IsInRoleAsync(user, "Administrator").Result && adminCount == 1)
            {
                ViewBag.Error = true;
            }
            else
            {
               ViewBag.Error = false;
            }

            return View(editUserViewModel);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel editUserViewModel)
        {
            editUserViewModel.Roles = _roleManager.Roles;
            if (ModelState.IsValid)
            {
                IdentityUser user = _userManager.FindByIdAsync(editUserViewModel.Id).Result;
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;

                IdentityResult result = _userManager.UpdateAsync(user).Result;

                if (result.Succeeded)
                {
                    if (editUserViewModel.CurrentRole != editUserViewModel.SelectedRole && editUserViewModel.SelectedRole != null)
                    {
                        var removeRoleResult = _userManager.RemoveFromRoleAsync(user, editUserViewModel.CurrentRole).Result;
                        var addRoleResult = _userManager.AddToRoleAsync(user, editUserViewModel.SelectedRole).Result;
                    }

                    return RedirectToAction("UserManagement", _userManager.Users);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        string propertyName = string.Empty;
                        string errorDescription = string.Empty;
                        if (error.Code.Contains("DuplicateEmail"))
                        {
                            propertyName = "Email";
                            errorDescription = _sharedLocalizer["ErrorDuplicateRecord"];
                        }

                        if (error.Code.Contains("DuplicateUserName"))
                        {
                            propertyName = "UserName";
                            errorDescription = _sharedLocalizer["ErrorDuplicateRecord"];
                        }

                        ModelState.AddModelError(propertyName, errorDescription);
                    }

                    return View(editUserViewModel);
                }
            }

            return View(editUserViewModel);
        }

        public IActionResult ChangePassword(string userId)
        {
            IdentityUser user = _userManager.FindByIdAsync(userId).Result;
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return View(changePasswordViewModel);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = _userManager.FindByNameAsync(changePasswordViewModel.UserName).Result;
                if (user != null)
                {
                    var passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<IdentityUser>)) as
                            IPasswordValidator<IdentityUser>;

                    var passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<IdentityUser>)) as
                            IPasswordHasher<IdentityUser>;
                    user.PasswordHash = passwordHasher.HashPassword(user, changePasswordViewModel.Password);

                    IdentityResult result = passwordValidator
                        .ValidateAsync(_userManager, user, changePasswordViewModel.Password).Result;

                    // IdentityResult result = _userManager.UpdateAsync(user).Result;

                    if (result.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, changePasswordViewModel.Password);
                        _userManager.UpdateAsync(user);
                        return RedirectToAction("UserManagement", _userManager.Users);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(changePasswordViewModel);
                    }
                }
            }

            return View(changePasswordViewModel);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIfUserNameIsUnique(string UserName)
        {
            var user = _userManager.FindByNameAsync(UserName).Result;
            return user == null ? Json(true) : Json(_sharedLocalizer["ErrorDuplicateRecord"]);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIfEmailIsUnique(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            return user == null ? Json(true) : Json(_sharedLocalizer["ErrorDuplicateRecord"]);
        }

        public IActionResult RoleManagement()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = addRoleViewModel.Name
                };

                IdentityResult result = _roleManager.CreateAsync(role).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement");
                }
                else
                {
                    foreach (var resultError in result.Errors)
                    {
                        if (resultError.Code == "DuplicateRoleName")
                        {
                            ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                        }
                    }
                }
            }

            return View(addRoleViewModel);
        }

        public IActionResult EditRole(string roleId)
        {
            IdentityRole role = _roleManager.FindByIdAsync(roleId).Result;
            EditRoleViewModel editRoleViewModel = new EditRoleViewModel()
            {
                Name = role.Name,
                Id = role.Id
            };
            return View(editRoleViewModel);
        }

        [HttpPost]
        public IActionResult EditRole(EditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = _roleManager.FindByIdAsync(editRoleViewModel.Id).Result;

                role.Name = editRoleViewModel.Name;
                IdentityResult result = _roleManager.UpdateAsync(role).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement");
                }
                else
                {
                    foreach (var resultError in result.Errors)
                    {
                        if (resultError.Code == "DuplicateRoleName")
                        {
                            ModelState.AddModelError("Name", _sharedLocalizer["ErrorDuplicateRecord"]);
                        }
                    }
                }
            }

            return View(editRoleViewModel);
        }

        public IActionResult DeleteRole(string roleId)
        {
            IdentityRole role = _roleManager.FindByIdAsync(roleId).Result;
            return View(role);
        }

        [HttpPost]
        public IActionResult DeleteRole(IdentityRole identityRole)
        {
            IdentityRole role = _roleManager.FindByIdAsync(identityRole.Id).Result;

            if (role != null)
            {
                IdentityResult result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement");
                }
                else
                {
                    ModelState.AddModelError("", _sharedLocalizer["ErrorDbUpdate"]);
                    return View(identityRole);
                }
            }
            else
            {
                return RedirectToAction("RoleManagement");
            }
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckIfRoleNameIsUnique(string name)
        {
            var role = _roleManager.FindByNameAsync(name).Result;
            return role == null ? Json(true) : Json(_sharedLocalizer["ErrorDuplicateRecord"]);
        }
    }
}