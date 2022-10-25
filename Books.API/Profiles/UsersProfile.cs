﻿using AutoMapper;
using Books.API.Entities;
using Books.API.Models.Dto;
using System;

namespace Books.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<LoginRequestDto, LocalUser>();

            CreateMap<(ApplicationUser LocalUser, string Token), LoginResponseDto>()
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.LocalUser))
                  .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            CreateMap<LoginRequestDto, ApplicationUser>();

            CreateMap<RegisterationRequestDto, ApplicationUser>();

            CreateMap<ApplicationUser, RegisterationResponsetDto>();
        } 
    }
}
