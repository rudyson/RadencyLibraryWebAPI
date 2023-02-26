namespace RadencyLibraryWebAPI.Models.DTO
{
	public class BookDetailedDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Author { get; set; }
		public string? Cover { get; set; }
		public string? Content { get; set; }
		public decimal? Rating { get; set; }
		public List<ReviewDetailedDto>? Reviews { get; set; }
	}
}
