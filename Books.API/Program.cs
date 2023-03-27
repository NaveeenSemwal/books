using Books.Business.Model.Configuration;
using Books.Core;
using Books.Core.Seeds;
using Books.Data.EntityFramework.Contexts;
using Books.Data.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Books.API
{
    public class Program
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=_iryZxv8Rxw
        /// https://github.com/serilog/serilog-aspnetcore/blob/dev/samples/Sample/Program.cs
        /// https://www.youtube.com/watch?v=a68X_9CuUkw
        /// </summary>
        /// <param name="args"></param>
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings    
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            IConfigurationRoot configuration = Configuration.BuildConfiguration(Assembly.GetExecutingAssembly(), args);

            //Initialize Logger    
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting web host {environment} ", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production);

                Log.Information("Book Connection {ConnectionString}", configuration.GetConfigurationSection<BooksDatabaseConfiguration>("BooksDatabaseConfiguration").ConnectionString);


                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var bookContext = services.GetRequiredService<BookContext>();
                    await bookContext.Database.MigrateAsync(); // Apply pending migration or create DB.
                    await Seed.SeedBooks(bookContext);

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    await DefaultRoles.SeedAsync(roleManager);

                    await DefaultUsers.SeedUsers(userManager, roleManager);

                }

                Log.Information("Application Starting.");

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start." + ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseSerilog() //Uses Serilog instead of default .NET Logger 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
