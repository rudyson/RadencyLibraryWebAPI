namespace RadencyLibraryWebAPI.Models
{
	public class Rating
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public decimal Score { get; set; }
	}
}
