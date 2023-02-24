namespace RadencyLibraryWebAPI.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? Reviewer { get; set; }
        public string? Message { get; set; }
        // Relationships
        public Book? Book { get; set; }
    }
}
