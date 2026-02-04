using BooksApi.Dtos.Publisher;

namespace BooksApi.Services
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherWithBooksAndAuthorsDto?>> GetAllPublishers();
        Task<PublisherWithBooksAndAuthorsDto?> GetPublisherWithBooksAndAuthors(int publisherId);

        Task<int> CreatePublisherAsync(CreatePublisherDto dto);

        Task<bool> UpdatePublisherAsync(int publisherId, CreatePublisherDto dto);

        Task<bool> DeletePublisherAsync(int publisherId);
    }
}
