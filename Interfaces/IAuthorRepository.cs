using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IAuthorRepository:IGenericRepository<Author>
    {
        Task<Author> GetAuthorWithBooks(int id);
    }
}
