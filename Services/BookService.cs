using AutoMapper;
using BooksApi.Dtos.Book;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {                 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateBookAsync(CreateBookDto dto)
        {
            if(dto.AuthorIds == null || !dto.AuthorIds.Any())
            {
                throw new ArgumentException("At least one author ID must be provided.", nameof(dto.AuthorIds));
            }

            var book = _mapper.Map<Book>(dto);

            book.BookAuthors = dto.AuthorIds.Select(authorId => new BookAuthor
            {
                AuthorId = authorId
            }).ToList();

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.Save();

            return book.Id;


        }
    }
}
