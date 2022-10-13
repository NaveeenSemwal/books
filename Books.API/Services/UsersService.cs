using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.API.Services.Implementation;
using System;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public bool IsUniqueUser(string userName)
        {
            return _userRepository.IsUniqueUser(userName);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            return await _userRepository.Login(loginRequestDto);
        }

        public async Task<LocalUser> Register(RegisterationRequestDto registerationRequestDto)
        {
            return await _userRepository.Register(registerationRequestDto);
        }
    }
}
