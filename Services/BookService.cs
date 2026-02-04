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

        public async Task<IEnumerable<GetBookDto?>> GetAllBooks()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var booksdto = _mapper.Map<IEnumerable<GetBookDto>>(books);

            return booksdto;


        }

        public async Task<GetBookDto?> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            if (book == null) return null;
           
            var bookDto = _mapper.Map<GetBookDto>(book);
            return bookDto;

        }

        public async Task<GetBookDto?> GetBookWithAuthorsAsync(int id)
        {
            var book = await _unitOfWork.Books.GetBookWithAuthors(id);
            if (book == null) return null;
            var bookDto = _mapper.Map<GetBookDto>(book);
            return bookDto;
        }


        public async Task<int> CreateBookAsync(CreateBookDto dto)
        {
            if(dto.AuthorIds == null || !dto.AuthorIds.Any())
            {
                throw new ArgumentException("At least one author ID must be provided.", nameof(dto.AuthorIds));
            }

            if(dto.PublisherId <= 0)
            {
                throw new ArgumentException("A valid publisher ID must be provided.", nameof(dto.PublisherId));
            }

            foreach(int authorId in dto.AuthorIds)
            {
                var authorExists = await _unitOfWork.Authors.GetByIdAsync(authorId);
                if(authorExists  is null)
                {
                    throw new ArgumentException($"Author with ID {authorId} does not exist.", nameof(dto.AuthorIds));
                }
                
               
                
            }

            var publisherExists = await _unitOfWork.Publishers.GetByIdAsync(dto.PublisherId);
            if (publisherExists is null)
                throw new ArgumentException($"Publisher with ID {dto.PublisherId} does not exist.", nameof(dto.PublisherId));

            var book = _mapper.Map<Book>(dto);


            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.Save();
            
            return book.Id;

        }

        public async Task<bool> UpdateBook(int id, UpdateBookDto dto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return false;

            _mapper.Map(dto, book);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.Save();

            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return false;

            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.Save();
            return true;
        }
    }
}
