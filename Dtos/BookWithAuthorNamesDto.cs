namespace BooksApi.Dtos
{
    public class BookWithAuthorNamesDto // Book
    {
        public string Name { get; set; }   
        public List<string> Authors { get; set; }
    }
}
