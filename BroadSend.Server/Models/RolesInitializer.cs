using Microsoft.AspNetCore.Identity;

namespace BroadSend.Server.Models
{
    public class RolesInitializer
    {
        public static void Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "Admin";
            string adminEmail = "admin@broadsend.com";
            string adminPassword = "asdASD123!@#";
            string adminRole = "Administrator";
            string userRole = "User";

            if (roleManager.FindByNameAsync(adminRole).Result == null)
            {
                IdentityResult _ = roleManager.CreateAsync(new IdentityRole(adminRole)).Result;
            }

            if (roleManager.FindByNameAsync(userRole).Result == null)
            {
                IdentityResult _ = roleManager.CreateAsync(new IdentityRole(userRole)).Result;
            }

            if (userManager.FindByNameAsync(adminName).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = adminName,
                    Email = adminEmail
                };

                IdentityResult result = userManager.CreateAsync(user, adminPassword).Result;

                if (result.Succeeded)
                {
                    IdentityResult _ = userManager.AddToRoleAsync(user, adminRole).Result;
                }
            }
        }
    }
}
