using Books.Data.EntityFramework.Contexts;
using Books.Data.Interfaces;
using Books.Data.Model;
using Microsoft.Extensions.Logging;

namespace Books.Data.EntityFramework
{
    public class RolesRepository : Repository<ApplicationRole>, IRolesRepository
    {
        public RolesRepository(BookContext dbContext, ILoggerFactory logger) : base(dbContext, logger)
        {
        }

    }
}
