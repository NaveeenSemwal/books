using Books.API.Entities;
using Books.API.Models.Dto;
using Books.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Books.API.Services.Abstract
{
    public interface IUsersService
    {
        bool IsUniqueUser(string userName);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<RegisterationResponsetDto> Register(RegisterationRequestDto registerationRequestDto);

        Task<PagedList<MemberDto>> GetAll(SearchParams searchParams);

        Task<MemberDto> Get(string username);

        Task<bool> UpdateUser(MemberUpdateDto memberUpdateDto);
    }
}
