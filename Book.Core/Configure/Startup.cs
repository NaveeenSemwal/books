﻿using Books.API.Contexts;
using Books.API.Services;
using Books.API.Services.Abstract;
using Books.Core.Repositories.Abstract;
using Books.Core.Repositories.Implementation.Dapper;
using Books.Core.Repositories.Implementation.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Configure
{
    public static class Startup
    {

        /// <summary>Configure App repositories</summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Application configuration</param>
        public static void ConfigureAppRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repo should be AddScoped
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRolesRepository, RolesRepository>();

            var connectionString = configuration["ConnectionStrings:BooksDBConnectionString"];
            services.AddDbContext<BookContext>(o => o.UseSqlServer(connectionString));

            services.AddSingleton<DapperContext>();
        }
    }
}


