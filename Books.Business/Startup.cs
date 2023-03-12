using Books.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Books.Business
{
    public static class Startup
    {
        /// <summary>Configure App Services</summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // http request should be AddScoped

            services.AddScoped<IBooksServive, BooksServive>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITokenService, TokenService>();
        }

    }
}
