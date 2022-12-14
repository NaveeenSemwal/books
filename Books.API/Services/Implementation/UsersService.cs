using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using Books.API.Services.Abstract;
using Books.Core.Entities;
using Books.Core.Extensions;
using Books.Core.Helpers;
using Books.Core.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace Books.API.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        /// <summary>
        /// Get the user from HTTpContext
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MemberDto> Get(string username)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, false, includeProperties: "Photos");

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<PagedList<MemberDto>> GetAll(SearchParams searchParams)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(searchParams, includeProperties: "Photos");

            // Sending Pagination in header in Response
            _httpContextAccessor.HttpContext.Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalPages, users.TotalCount));

            return _mapper.Map<PagedList<MemberDto>>(users);
        }

        public bool IsUniqueUser(string userName)
        {
            return _unitOfWork.UserRepository.IsUniqueUser(userName);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(loginRequestDto);

            var localuser = await _unitOfWork.UserRepository.Login(user, loginRequestDto.Password);

            return _mapper.Map<LoginResponseDto>(localuser);
        }

        public async Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser localUser = _mapper.Map<ApplicationUser>(registerationRequestDto);

            var response = new RegisterationResponsetDto();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _userManager.CreateAsync(localUser, registerationRequestDto.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(localUser, registerationRequestDto.Role);

                        var userToReturn = await _unitOfWork.UserRepository.GetAsync(x => x.UserName.ToLower() == registerationRequestDto.Username.ToLower(), false);

                        scope.Complete();

                        return _mapper.Map<RegisterationResponsetDto>(userToReturn);
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            response.ErrorMessages.Add(item.Description);
                        }
                    }
                }
                catch (Exception)
                {

                    scope.Dispose();
                    throw;
                }
            }

            return response;
        }

        public async Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == userName, true);

            if (user == null) return false;

            // No Need to call the Update function of EF. AutoTracking & mapper works for you.
            _mapper.Map(memberUpdateDto, user);

            return await _unitOfWork.Complete();

        }
    }
}
