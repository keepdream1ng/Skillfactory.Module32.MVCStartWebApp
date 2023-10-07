using Microsoft.EntityFrameworkCore;
using Skillfactory.Module32.ASPCoreMVC.Middleware;

namespace Skillfactory.Module32.MVCStartWebApp;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureServices();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseMiddleware<LoggingMiddleware>();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Get the connection string from appsettings.json
        var config = builder.GetConfiguration();
        string connection = config.GetConnectionString("DefaultConnection");
        // Add services to the container.
        builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
        builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
    }

    private static IConfigurationRoot GetConfiguration(this WebApplicationBuilder builder)
    {
        // Load configuration from appsettings.json
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();
    }

}