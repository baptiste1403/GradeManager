using GradeManager.Models;
using Microsoft.AspNetCore.Identity;

namespace GradeManager.Data
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            // 1. Resolve our managers from the temporary scope container
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 2. Define the core default roles you want to exist out-of-the-box
            string[] roles = ["Admin", "Teacher", "Student"];

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 3. Define and seed your default super-admin user account
            var adminEmail = "admin@admin.fr";
            var defaultAdminUser = await userManager.FindByEmailAsync(adminEmail);

            if (defaultAdminUser == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Administrator"
                };

                // Choose a strong default password for local development
                var createAdminResult = await userManager.CreateAsync(adminUser, "SecureAdminPassword123!");

                if (createAdminResult.Succeeded)
                {
                    // Assign the newly created account to the Admin role
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

