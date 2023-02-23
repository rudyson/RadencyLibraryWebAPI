namespace RadencyLibraryWebAPI.Models
{
	public class Review
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string? Reviewer { get; set; }
	}
}
