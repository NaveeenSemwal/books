using Books.API.ActionFilters;
using Books.API.BackgroundJob;
using Books.API.Configure;
using Books.API.Middlewares;
using Books.Business;
using Books.Business.Model.Configuration;
using Books.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Books.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCores(Configuration);

            // Added AddNewtonsoftJson() for HTTPPUT verb
            services.AddControllers().AddNewtonsoftJson();

            services.ConfigureAppRepositories(Configuration);

            services.ConfigureIdentityCore(Configuration);

            services.ConfigureAppServices(Configuration);

            services.AddScoped<LogUserActivity>();

            services.ConfigureJwtAuthentication(Configuration);

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.ConfigureSwagger(Configuration);


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

            services.AddHostedService<BookBackgroundService>();


            // Add API versioning
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });


            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.SubstituteApiVersionInUrl = true; // This will show the version in URL
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORS");

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
            app.UseSwaggerUI(options =>
            {

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Books Services  V1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Books Services  V2");


            });
        }
    }
}
