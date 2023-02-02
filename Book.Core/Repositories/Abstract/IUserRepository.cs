using Books.API.Entities;
using System;
using System.Threading.Tasks;

namespace Books.API.Services.Abstract
{
    public interface IUserRepository : IRepository<Entities.ApplicationUser>
    {
       
    }
}
