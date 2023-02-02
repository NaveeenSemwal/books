using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using Books.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Books.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {

            CreateMap<(ApplicationUser LocalUser, string Token), LoginResponseDto>()
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.LocalUser))
                  .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            CreateMap<LoginRequestDto, ApplicationUser>();

            CreateMap<RegisterationRequestDto, ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Username + "@gmail.com"));

            CreateMap<ApplicationUser, RegisterationResponsetDto>();

            CreateMap<ApplicationUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain == true).Url))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

            // Automapper with inherited list
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(CustomConverter<,>));

            CreateMap<MemberUpdateDto, ApplicationUser>();
        }
    }
}
