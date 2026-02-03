using AutoMapper;
using BooksApi.Dtos.Author;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {                    
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AuthorDto?>> GetAllAuthors()
        {
           var authors = await _unitOfWork.Authors.GetAllAsync();

            var authorsDto = _mapper.Map<IEnumerable<AuthorDto?>>(authors);
            return authorsDto;
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);

            if(author is null) return null;

            var authorDto = _mapper.Map<AuthorDto?>(author);

            return authorDto;

        }

        public async Task<AuthorWithBooksDto?>GetAuthorWithBooksAsync(int id)
        {
           var author = await _unitOfWork.Authors.GetAuthorWithBooks(id);
            if(author is null) return null;
            var authorWithBooksDto = _mapper.Map<AuthorWithBooksDto?>(author);
            return authorWithBooksDto;
        }
        public async Task<int> CreateAuthorAsync(AuthorDto dto)
        {
            if(string.IsNullOrEmpty(dto.Name))
            {
                throw new ArgumentException("Author name cannot be null or empty");
            }

            var author = _mapper.Map<Author>(dto);

            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.Save();
            return author.Id;
        }
        public async Task<bool> UpdateAuthor(int id, AuthorDto dto)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);

            if(author is null) return false;

            _mapper.Map(dto, author);
            await _unitOfWork.Authors.UpdateAsync(author);
            await _unitOfWork.Save();
            return true;
        }
        public async Task<bool> DeleteAuthor(int id)
        {
           var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if(author is null) return false;
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.Save();
            return true;
        }

     

       
    }
}
