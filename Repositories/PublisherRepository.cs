using BooksApi.Data;
using BooksApi.Interfaces;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _context;

        public PublisherRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<Publisher?> GetPublisherWithBooksAndAuthors(int publisherId)
        {
            var publisher = await _context.Publishers
                .Include(b=>b.Books)
                .ThenInclude(x=>x.BookAuthors)
                .ThenInclude(x=>x.Author)
                .FirstOrDefaultAsync(p => p.Id == publisherId);
            return publisher;
        }
    }
}
