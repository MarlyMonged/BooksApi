using BooksApi.Dtos.Book;
using BooksApi.Models;

namespace BooksApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<GetBookDto?>> GetAllBooks();
        Task<GetBookDto?> GetBookByIdAsync(int id);
        Task<GetBookDto?>GetBookWithAuthorsAsync(int id);
        Task<int> CreateBookAsync(CreateBookDto dto);

        Task<bool>UpdateBook(int id, UpdateBookDto dto);

        Task<bool> DeleteBook(int id);
    }
}
