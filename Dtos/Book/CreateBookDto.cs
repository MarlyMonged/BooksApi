namespace BooksApi.Dtos.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; } = 0;
        public string Genre { get; set; } = string.Empty;
      
        public string CoverUrl { get; set; } = string.Empty;

        public int PublisherId { get; set; }

        public List<int> AuthorIds { get; set; } 
    }
}
