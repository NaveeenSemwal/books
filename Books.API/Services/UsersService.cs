using AutoMapper;
using Books.API.Entities;
using Books.API.Migrations;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
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
            var user = _mapper.Map<Entities.LocalUser>(loginRequestDto);

            var localuser = await _userRepository.Login(user);

            return _mapper.Map<LoginResponseDto>(localuser);
        }

        public async Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            var userRequest = _mapper.Map<Entities.LocalUser>(registerationRequestDto);

            var user = await _userRepository.Register(userRequest);

            return _mapper.Map<RegisterationResponsetDto>(user);
        }
    }
}
