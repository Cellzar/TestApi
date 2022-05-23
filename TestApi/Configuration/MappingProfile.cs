using AutoMapper;
using TestDB.Context;
using TestDB.DTOs;

namespace TestApi.Configuration
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
        }
    }
}
