using Books.API.Contexts;
using Books.API.Middlewares;
using Books.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Books.API.Services.Abstract;
using Books.Core.Repositories.Implementation.EntityFramework;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Books.API.Entities;
using Books.Core.Entities;
using Books.Core.Repositories.Implementation.Dapper;

namespace Books.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            // Added AddNewtonsoftJson() for HTTPPUT verb
            services.AddControllers().AddNewtonsoftJson();

            var connectionString = Configuration["ConnectionStrings:BooksDBConnectionString"];
            services.AddDbContext<BookContext>(o => o.UseSqlServer(connectionString));

            // Repo should be AddScoped
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IBooksServive, BooksServive>();
            services.AddTransient<IUsersService, UsersService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Books Info Service API",
                    Version = "v1",
                    Description = "Sample service for Books",
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[SECRET USED TO SIGN AND VERIFY JWT TOKENS, IT CAN BE ANY STRING]")),

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });

            /* .AddMicrosoftIdentityWebApi(Configuration); Add this line to Protect the WebAPI using Microsoft Identity (Azure AD). This code itself will do token validation.*/

            /*
             * 
             * 
             *------------ Protect the WebAPI using Microsoft Identity (Azure AD). This code itself will do token validation.
             * 
             * 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration);
            */


            // Adding Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(o => o.User.RequireUniqueEmail = true).AddEntityFrameworkStores<BookContext>();

            services.AddSingleton<DapperContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Books Services"));
        }
    }
}
