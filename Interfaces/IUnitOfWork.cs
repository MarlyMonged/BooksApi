using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
       IGenericRepository<Book> Books {  get; }

        Task<int> Save();
    }
}
