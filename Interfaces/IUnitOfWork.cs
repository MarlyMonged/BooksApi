using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
       IBookRepository Books {  get; }
       IGenericRepository<Author> Authors {  get; }
       IGenericRepository<Publisher> Publishers {  get; }

        Task<int> Save();
    }
}
