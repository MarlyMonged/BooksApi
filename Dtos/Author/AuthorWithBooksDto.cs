namespace BooksApi.Dtos.Author
{
    public class AuthorWithBooksDto
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Books { get; set; } = new List<string>();
    }
}
