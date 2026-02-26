namespace BooksApi.ViewModels
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireAt { get; set; }
        
    }
}
