
using Books.Data.EntityFramework.Contexts;
using Books.Data.Interfaces;
using Books.Data.Model;
using Microsoft.Extensions.Logging;

namespace Books.Data.EntityFramework
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(BookContext dbContext, ILoggerFactory logger) : base(dbContext, logger)
        {
        }
    }
}
