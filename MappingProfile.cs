using AutoMapper;
using Azure.Identity;
using BooksApi.Dtos.Book;
using BooksApi.Models;

namespace BooksApi
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, GetBookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
