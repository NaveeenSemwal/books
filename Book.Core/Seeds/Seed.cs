using Books.API.Contexts;
using Books.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Core.Seeds
{
    public class Seed
    {
        public static async Task SeedBooks(BookContext context)
        {
            if (!await context.Books.AnyAsync())
            {
                var bookData = await File.ReadAllTextAsync("Configure/BookSeedData.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var books = JsonSerializer.Deserialize<List<Book>>(bookData);

                foreach (var book in books)
                {
                    context.Books.Add(book);
                }

                await context.SaveChangesAsync();
            }
        }


        public static async Task SeedUsers(BookContext context, UserManager<ApplicationUser> userManager)
        {
            if (!await context.ApplicationUsers.AnyAsync())
            {
                var userData = await File.ReadAllTextAsync("Configure/UserSeedData.json");

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData);

                foreach (var user in users)
                {
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
                    context.ApplicationUsers.Add(user);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
