using BooksApi.Dtos.Author;

namespace BooksApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto?>> GetAllAuthors();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorWithBooksDto?> GetAuthorWithBooksAsync(int id);
        Task<int> CreateAuthorAsync(AuthorDto dto);
        Task<bool> UpdateAuthor(int id, AuthorDto dto);
        Task<bool> DeleteAuthor(int id);
    }
}
