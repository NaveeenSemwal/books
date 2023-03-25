using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Books.Core
{

    public static class Configuration
    {
        /// <summary>
        /// Extension method to build and access configuration from multiple resources.
        /// </summary>
        /// <param name="executingAssembly"></param>
        /// <param name="args"></param>
        /// <param name="jsonFiles"></param>
        /// <returns></returns>
        public static IConfigurationRoot BuildConfiguration(Assembly executingAssembly, string[] args = null, string[] jsonFiles = null)
        {
            string text = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(text))
            {
                text = Environments.Production;
            }

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings." + text + ".json", optional: true);

            if (jsonFiles != null)
            {
                foreach (string path in jsonFiles)
                {
                    builder.AddJsonFile(path);
                }
            }

            builder.AddUserSecrets(executingAssembly, optional: true);
            builder.AddEnvironmentVariables();

            if (args != null && args.Any())
            {
                builder.AddCommandLine(args);
            }

            return builder.Build();
        }


        /// <summary>
        /// Get a Section from the Configuration and deserlise to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static T GetConfigurationSection<T>(this IConfigurationRoot configuration , string sectionName)
        {
            return (configuration.GetSection(sectionName) ?? throw new ApplicationException("section not defined")).Get<T>();
        }
    }
}
