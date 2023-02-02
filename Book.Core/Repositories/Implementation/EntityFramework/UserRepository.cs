using Books.API.Contexts;
using Books.API.Entities;
using Books.API.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Repositories.Implementation.EntityFramework
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BookContext dbContext, ILoggerFactory logger) : base(dbContext, logger)
        {
        }
    }
}
