using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using BroadSend.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace BroadSend.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    RolesInitializer.Initialize(userManager, roleManager);
                    Log.Information("Database initialization");
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Initialization of Database has failed");
;                }
            }

            try
            {
                Log.Information("Application starting up.");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application has failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}