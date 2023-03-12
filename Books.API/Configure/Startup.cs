using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Books.API.Configure
{
    public static class Startup
    {
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

            //services.AddIdentityCore<ApplicationUser>(opt => opt.User.RequireUniqueEmail = true).AddRoles<ApplicationRole>()
            //    .AddRoleManager<RoleManager<ApplicationRole>>()
            //    .AddEntityFrameworkStores<BookContext>();

            // Note : This is already covered in -- public static void ConfigureIdentityCore(this IServiceCollection services, IConfiguration configuration)


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


