using BooksApi.Models;

namespace BooksApi.Interfaces
{
    public interface IPublisherRepository:IGenericRepository<Publisher>
    {
        Task<Publisher?> GetPublisherWithBooksAndAuthors(int publisherId);
    }
}
