using Books.API.Entities;
using System;
using System.Threading.Tasks;

namespace Books.API.Services.Abstract
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string userName);

        Task<(LocalUser LocalUser, string Token)> Login(LocalUser localUser);

        Task<LocalUser> Register(LocalUser localUser);
    }
}
