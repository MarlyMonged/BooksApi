using BooksApi.Data;
using BooksApi.Interfaces;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public async Task<Author> GetAuthorWithBooks(int id)
        {
           var book = await _context.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return book!;
        }
    }
}
