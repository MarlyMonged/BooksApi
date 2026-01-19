namespace BooksApi.Dtos.Book
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; } = 0;
        public string Genre { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
    }
}
