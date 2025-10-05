using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

public class RoleManagerSeeder
{
    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
        string[] roleNames = ["Customer", "Admin", "StoreManager"];

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new AppRole{Name = roleName});
            }
        }
    }
}