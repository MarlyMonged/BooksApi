using BooksApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.ViewModels
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }  
        public bool IdRevoked { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime dateExpire { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
         public ApplicationUser User { get; set; }



    }
}
