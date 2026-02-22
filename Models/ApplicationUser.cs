using Microsoft.AspNetCore.Identity;

namespace BooksApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Custom { get; set; } 
    }
}
