using Blue.Domain.Users;
using Blue.Infrastructure.Persistence;
using Blue.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blue.Infrastructure.Seed;

public static class AdminSeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        if (context.Users.Any(u => u.Role == UserRole.Admin))
            return;

        var admin = User.CreateAdmin(
            name: "Admin",
            email: "admin@blue.com",
            passwordHash: hasher.Hash("Admin123!")
        );

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}