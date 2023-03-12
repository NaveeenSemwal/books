using AutoMapper;
using Books.Business.Model;
using Books.Business.Model.Request;
using Books.Business.Model.Response;
using Books.Core.Helpers;
using System.Linq;

namespace Books.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {

            CreateMap<(Data.Model.ApplicationUser LocalUser, string Token), LoginResponseDto>()
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.LocalUser))
                  .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            CreateMap<LoginRequestDto, Data.Model.ApplicationUser>();

            CreateMap<RegisterationRequestDto, Data.Model.ApplicationUser>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Username + "@gmail.com"));

            CreateMap<Data.Model.ApplicationUser, RegisterationResponsetDto>();

            CreateMap<Data.Model.ApplicationUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain == true).Url))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

            // Automapper with inherited list
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(CustomConverter<,>));

            CreateMap<MemberUpdateDto, Data.Model.ApplicationUser>();
        }
    }
}
