using AutoMapper;
using BooksApi.Data;
using BooksApi.Dtos.Book;
using BooksApi.Interfaces;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BooksApi.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Book> GetBookWithAuthors(int id)
        {
            var book = await _context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return book!;

            
        }
    }
}
