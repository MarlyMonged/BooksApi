using AutoMapper;
using Azure.Identity;
using BooksApi.Dtos;
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
            CreateMap<CreateBookDto, Book>()
                .ForMember(des=>des.BookAuthors,
                opt=>opt.MapFrom(src=>src.AuthorIds.Select(authorId =>new BookAuthor{ AuthorId=authorId}).ToList()));
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Author, AuthorDto>().ReverseMap();

            CreateMap<Author, AuthorWithBooksDto>()
                .ForMember(des => des.Books,
                opt => opt.MapFrom(src => src.BookAuthors.Select(b => b.Book.Title)));

            CreateMap<CreatePublisherDto, Publisher>();

            CreateMap<Publisher, PublisherWithBooksAndAuthorsDto>()
                .ForMember(dest => dest.Books,
                opt => opt.MapFrom(src => src.Books));
                

            CreateMap<Book, BookWithAuthorNamesDto>()
                .ForMember(des=>des.Name,
                opt=>opt.MapFrom(src=>src.Title))
                .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src => src.BookAuthors.Select(x => x.Author.Name).ToList()));
        }
    }
}
