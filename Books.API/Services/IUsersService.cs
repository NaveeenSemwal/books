using Books.API.Entities;
using Books.API.Models.Dto;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public interface IUsersService
    {
        bool IsUniqueUser(string userName);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<LocalUser> Register(RegisterationRequestDto registerationRequestDto);
    }
}
