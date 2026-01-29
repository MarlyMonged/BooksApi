using AutoMapper;
using Azure.Identity;
using BooksApi.Dtos.Author;
using BooksApi.Dtos.Book;
using BooksApi.Dtos.Publisher;
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
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<CreatePublisherDto, Publisher>();
        }
    }
}
