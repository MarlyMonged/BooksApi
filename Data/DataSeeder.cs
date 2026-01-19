using BooksApi.Models;

namespace BooksApi.Data
{
    public class DataSeeder
    {
        public static void SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if(!context.Books.Any())
            {
                context.Books.AddRange(new Book
                {
                    Title = "Title 1",
                    Description = "Description 1",
                    IsRead = true,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Biography",
                    Author = "Author 1",
                    CoverUrl = "https://example.com/firstbookcover.jpg",
                    DateAdded = DateTime.Now
                },
                new Book
                {
                    Title = "Title 2",
                    Description = "Description 2",
                    IsRead = false,
                    Genre = "Science Fiction",
                    Author = "Author 2",
                    CoverUrl = "https://example.com/secondbookcover.jpg",
                    DateAdded = DateTime.Now
                });
                context.SaveChanges();

            }
        }   
    }
}
