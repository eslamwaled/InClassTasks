using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Session27Api.Entities;

namespace Session27Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Users.AnyAsync(u => u.Role == "Admin"))
        {
            return;
        }

        var admin = new User
        {
            Email = "admin@session27.com",
            UserName = "admin",
            Role = "Admin"
        };
        admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "Admin@12345");

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}
