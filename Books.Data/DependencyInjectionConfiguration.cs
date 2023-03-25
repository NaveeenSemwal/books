using Books.Data.Dapper;
using Books.Data.EntityFramework;
using Books.Data.EntityFramework.Contexts;
using Books.Data.Interfaces;
using Books.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Data
{
    public static class DependencyInjectionConfiguration
    {
        /// <summary>Configure App repositories</summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        public static void ConfigureAppRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var connectionString = configuration["BooksDatabaseConfiguration:ConnectionString"];
            services.AddDbContext<BookContext>(o => o.UseSqlServer(connectionString));

            services.AddSingleton<DapperContext>();
        }

        public static void ConfigureIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUser>(opt => opt.User.RequireUniqueEmail = true).AddRoles<ApplicationRole>()
              .AddRoleManager<RoleManager<ApplicationRole>>()
              .AddEntityFrameworkStores<BookContext>();
        }
    }
}
