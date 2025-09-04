using Microsoft.AspNetCore.Identity;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        // Get the required services
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed Roles
        string[] roleNames = { "Admin", "Creator", "User", "Guest" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create a default Admin user
        var adminUser = await userManager.FindByEmailAsync("admin@brightlife.com");
        if (adminUser == null)
        {
            var newAdmin = new User
            {
                UserName = "admin@brightlife.com",
                Email = "admin@brightlife.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User"
            };
            var result = await userManager.CreateAsync(newAdmin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
