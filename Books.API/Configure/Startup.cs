using Books.API.ActionFilters;
using Books.API.Contexts;
using Books.API.Entities;
using Books.API.Services.Abstract;
using Books.API.Services.Implementation;
using Books.Core.Entities;
using Books.Core.Repositories.Implementation.Dapper;
using Books.Core.Repositories.Implementation.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.API.Configure
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

            services.AddScoped<LogUserActivity>();

        }


        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Order or sequence is important

            // Adding Identity

            /* Note : This line should always be above of -- services.AddAuthentication() otherwise JwtBearerDefaults.AuthenticationScheme will be ignored.
             *        AddIdentity() uses cookie based Authentication as default scheme. Otherwise your web-api-core-returns-404-when-adding-authorize-attribute.
             *        
             *        For validating JWT,JwtBearerDefaults.AuthenticationScheme is required.
             *        https://stackoverflow.com/questions/52038054/web-api-core-returns-404-when-adding-authorize-attribute
             */
            //services.AddIdentity<ApplicationUser, ApplicationRole>(o => o.User.RequireUniqueEmail = true).AddEntityFrameworkStores<BookContext>();

            services.AddIdentityCore<ApplicationUser>(opt => opt.User.RequireUniqueEmail = true).AddRoles<ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<BookContext>();

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.IncludeErrorDetails = true;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]")),

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));

            });


        }

        /// <summary>
        /// SwaggerDoc("v1", options.SwaggerDoc("v2"   must match with value in {{}}
        /// 
        ///   options.SwaggerEndpoint("/swagger/ {{v1}}/swagger.json", "Books Services  V1");
        ///   options.SwaggerEndpoint("/swagger/{{v2}}/swagger.json", "Books Services  V2");
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Books Info Service API v1",
                    Version = "v1.0",
                    Description = "Sample service for Books",
                });

                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Books Info Service API v2",
                    Version = "v2.0",
                    Description = "Sample service for Books",
                });
            });

        }


        public static void ConfigureCores(this IServiceCollection services, IConfiguration configuration)
        {
            string MyAllowSpecificOrigins = "CORS";

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                       policy =>
                                       {
                                           policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                       });
            });

        }
    }
}


