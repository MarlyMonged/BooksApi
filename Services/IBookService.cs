using BooksApi.Dtos.Book;

namespace BooksApi.Services
{
    public interface IBookService
    {
        Task<int> CreateBookAsync(CreateBookDto dto);
    }
}
