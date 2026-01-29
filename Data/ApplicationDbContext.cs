using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor>BookAuthors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
            
        
       
    }
}
