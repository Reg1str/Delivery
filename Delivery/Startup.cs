using Delivery.DataAccess.EntityFramework;
using Delivery.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Delivery;

public class Startup
{
    private IConfiguration _config;
    
    public Startup(IConfiguration config)
    {
        _config = config;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));
        
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>();
        
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
        });
        
        services.AddTransient<IRepository, Repository>();
        
        services.AddMvc(options => options.EnableEndpointRouting = false);
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseStaticFiles();
            
        app.UseAuthentication();

        app.UseMvcWithDefaultRoute();
    }
}