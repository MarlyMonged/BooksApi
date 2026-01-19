using BooksApi.Data;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Book> Books { get;private set;  }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
