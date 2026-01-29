using BooksApi.Data;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Book> Books { get;private set;  }

        public IGenericRepository<Author> Authors { get; private set; }

        public IGenericRepository<Publisher> Publishers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(_context);
            Authors = new GenericRepository<Author>(_context);
            Publishers = new GenericRepository<Publisher>(_context);
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
