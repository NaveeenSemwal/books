using AutoMapper;
using Books.Business.Model;
using Books.Data.Model;

namespace Books.API.Profiles
{
    public class PhotosProfile : Profile
    {
        public PhotosProfile()
        {
            CreateMap<Photo, PhotoDto>().ReverseMap();

        }
    }
}
