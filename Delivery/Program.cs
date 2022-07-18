using Delivery.DataAccess.EntityFramework;
using Delivery.Services.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace Delivery;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();
        try
        {
            var scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");
            if (!ctx.Roles.Any())
            {
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!ctx.Users.Any(u => u.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        host.Run();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
}