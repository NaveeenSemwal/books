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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

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
        private readonly ITokenService _tokenService;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public async Task<MemberDto> Get(string username)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == username, false, includeProperties: "Photos");

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<MemberDto> Get(object id)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(id);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<PagedList<MemberDto>> GetAll(UserParams searchParams)
        {
            // 2023- 100 = 1923 : This is MAX DOB year
            var minDob = DateTime.Today.AddYears(-searchParams.MaxAge);

            // 2023 - 18 = 2005 : This is MIN DOB year
            var maxDob = DateTime.Today.AddYears(-searchParams.MinAge);

            // Excluding current user and gender from result set.
            Expression<Func<ApplicationUser, bool>> filter = x => x.UserName != _httpContextAccessor.HttpContext.User.GetUserName()
            && x.Gender == searchParams.Gender && x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob;

            // Sorting users based on OrderBy param
            Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = x => searchParams.OrderBy switch
            {
                "created" => x.OrderByDescending(u => u.Created),
                _ => x.OrderByDescending(u => u.LastActive)
            };

            var users = await _unitOfWork.UserRepository.GetAllAsync(searchParams, filter, includeProperties: "Photos", orderBy);

            // Sending Pagination in header in Response
            _httpContextAccessor.HttpContext.Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize,
                users.TotalPages, users.TotalCount));

            return _mapper.Map<PagedList<MemberDto>>(users);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            if (user == null) return new LoginResponseDto() { Error = "Invalid Username" };

            var result = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!result) return new LoginResponseDto() { Error = "Invalid Password" };

            var roles = await _userManager.GetRolesAsync(user);

            var response = new LoginResponseDto
            {
                User = _mapper.Map<MemberDto>(user),
                Token = _tokenService.GenerateJwtToken(user, roles)
            };

            return response;
        }

        public async Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto)
        {
            var response = new RegisterationResponsetDto();

            if (await UserExists(registerationRequestDto))
            {
                response.ErrorMessages.Add("Username is taken");

                return response;
            }

            ApplicationUser localUser = _mapper.Map<ApplicationUser>(registerationRequestDto);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _userManager.CreateAsync(localUser, registerationRequestDto.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(localUser, "Member");

                        var userToReturn = await _userManager.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == registerationRequestDto.Username.ToLower());

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

        private async Task<bool> UserExists(RegisterationRequestDto registerationRequestDto)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == registerationRequestDto.Username.ToLower());
        }

        public async Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userName = _httpContextAccessor.HttpContext.User.GetUserName();

            var user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == userName, true);

            if (user == null) return false;

            // No Need to call the Update function of EF. AutoTracking & mapper works for you.
            _mapper.Map(memberUpdateDto, user);

            return await _unitOfWork.Complete();

        }
    }
}
