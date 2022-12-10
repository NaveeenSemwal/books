﻿using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.Core.Entities;
using Books.Core.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<MemberDto> Get(string username)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, false, includeProperties: "Photos");

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<IEnumerable<MemberDto>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(includeProperties: "Photos");

            var members = _mapper.Map<List<MemberDto>>(users);

            return members;
        }

        public bool IsUniqueUser(string userName)
        {
            return _unitOfWork.UserRepository.IsUniqueUser(userName);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _mapper.Map<Entities.ApplicationUser>(loginRequestDto);

            var localuser = await _unitOfWork.UserRepository.Login(user, loginRequestDto.Password);

            return _mapper.Map<LoginResponseDto>(localuser);
        }

        public async Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser localUser = _mapper.Map<ApplicationUser>(registerationRequestDto);

            var response = new RegisterationResponsetDto();

            var result = await _userManager.CreateAsync(localUser, registerationRequestDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(localUser, registerationRequestDto.Role);

                var userToReturn = await _unitOfWork.UserRepository.GetAsync(x => x.Email.ToLower() == registerationRequestDto.Email.ToLower(), false);

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
