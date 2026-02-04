using AutoMapper;
using BooksApi.Data;
using BooksApi.Interfaces;
using BooksApi.Models;

namespace BooksApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IBookRepository Books { get; private set; }

        public IAuthorRepository Authors { get; private set; }

        public IPublisherRepository Publishers { get; private set; }

       

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Books = new BookRepository(_context,_mapper);
            Authors = new AuthorRepository(_context);
            Publishers = new PublisherRepository(_context);
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
