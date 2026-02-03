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
            CreateMap<Book, GetBookDto>()
                .ForMember(des => des.Authors,
                opt => opt.MapFrom(src => src.BookAuthors.Select(b => b.Author.Name)));
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Author, AuthorDto>().ReverseMap();

            CreateMap<Author, AuthorWithBooksDto>()
                .ForMember(des => des.Books,
                opt => opt.MapFrom(src => src.BookAuthors.Select(b => b.Book.Title)));

            CreateMap<CreatePublisherDto, Publisher>();
        }
    }
}
