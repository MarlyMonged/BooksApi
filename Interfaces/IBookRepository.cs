using BooksApi.Dtos.Book;
using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IBookRepository:IGenericRepository<Book>
    {
        Task<Book> GetBookWithAuthors(int id);
    }
}
