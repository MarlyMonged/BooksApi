using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
       IBookRepository Books {  get; }
       IAuthorRepository Authors {  get; }
       IPublisherRepository Publishers {  get; }

        Task<int> Save();
    }
}
