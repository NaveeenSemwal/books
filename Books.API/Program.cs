using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

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
        public static void Main(string[] args)
        {

            //Read Configuration from appSettings    
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //Initialize Logger    
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

            try
            {
                Log.Information("Application Starting.");

                CreateHostBuilder(args).Build().Run();
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
