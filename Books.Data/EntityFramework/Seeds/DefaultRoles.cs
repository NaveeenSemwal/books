using Books.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace Books.Core.Seeds
{
    public enum Roles
    {
        Member,
        Admin,
        Moderator
    }

    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Member.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = Roles.Member.ToString() });
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = Roles.Admin.ToString() });
            }
            if (!await roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = Roles.Moderator.ToString() });
            } 
        }
    }
}
