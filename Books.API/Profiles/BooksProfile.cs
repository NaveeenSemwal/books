using AutoMapper;
using Books.Business.Model;
using Books.Business.Model.Request;
using Microsoft.AspNetCore.JsonPatch;

namespace Books.API.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Books.Data.Model.Book, Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src =>
                    $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<BookForCreation, Data.Model.Book>();


            CreateMap<JsonPatchDocument<BookForCreation>, JsonPatchDocument<Data.Model.Book>>();
        }
    }
}
