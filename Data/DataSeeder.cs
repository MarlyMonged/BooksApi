using BooksApi.Models;
using BooksApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;

namespace BooksApi.Data
{
    public class DataSeeder
    {
        public static void SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Publishers.Any())
            {
                context.Publishers.Add(
                    new Publisher { Name = "Marly" }
                   
                );
                context.SaveChanges();
            }


            if (!context.Books.Any())
            {
                context.Books.AddRange(new Book
                {
                    Title = "Title 1",
                    Description = "Description 1",
                    IsRead = true,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Biography",
                    CoverUrl = "https://example.com/firstbookcover.jpg",
                    DateAdded = DateTime.Now,
                    PublisherId = 1
                },
                new Book
                {
                    Title = "Title 2",
                    Description = "Description 2",
                    IsRead = false,
                    Genre = "Science Fiction",
                    CoverUrl = "https://example.com/secondbookcover.jpg",
                    DateAdded = DateTime.Now,
                    PublisherId = 1

                });
                context.SaveChanges();

            }
        }   

        public static async Task SeedRoles(IApplicationBuilder applicationbuilder)
        {
            using var serviceScope = applicationbuilder.ApplicationServices.CreateScope();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));

            if (!await roleManager.RoleExistsAsync(UserRoles.Author))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Author));
        }

    }
}
