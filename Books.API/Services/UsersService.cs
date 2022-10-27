using AutoMapper;
using Books.API.Entities;
using Books.API.Migrations;
using Books.API.Models;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersService(IUserRepository userRepository, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userRepository = userRepository;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string userName)
        {
            return _userRepository.IsUniqueUser(userName);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _mapper.Map<Entities.ApplicationUser>(loginRequestDto);

            var localuser = await _userRepository.Login(user, loginRequestDto.Password);

            return _mapper.Map<LoginResponseDto>(localuser);
        }

        public async Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser localUser = _mapper.Map<ApplicationUser>(registerationRequestDto);

            var response = new RegisterationResponsetDto();

            var result = await _userManager.CreateAsync(localUser, registerationRequestDto.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync(registerationRequestDto.Role))
                {
                    await _userManager.AddToRoleAsync(localUser, registerationRequestDto.Role);
                }
                else
                {
                    await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" });
                    await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" });

                }

                var userToReturn = await _userRepository.GetAsync(x => x.UserName.ToLower() == registerationRequestDto.Email.ToLower(), false);

                return _mapper.Map<RegisterationResponsetDto>(userToReturn);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.Description);
                }
            }

            return response;
        }
    }
}
