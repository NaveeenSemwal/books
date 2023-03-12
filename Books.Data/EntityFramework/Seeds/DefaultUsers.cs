using Books.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Books.Core.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var userData = await File.ReadAllTextAsync("Configure/UserSeedData.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData);

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();

                    await userManager.CreateAsync(user, "Dotvik@987");

                    if (user.UserName != "admin")
                    {
                        await userManager.AddToRoleAsync(user, "Member");
                    }
                    else
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Admin", "Moderator" });
                    }
                }
            }
        }
    }
}
