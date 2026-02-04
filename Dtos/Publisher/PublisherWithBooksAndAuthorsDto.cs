namespace BooksApi.Dtos.Publisher
{
    public class PublisherWithBooksAndAuthorsDto // Publisher
    {
        public string Name { get; set; }
        public List<BookWithAuthorNamesDto> Books { get; set; }
    }
}
