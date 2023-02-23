using Microsoft.EntityFrameworkCore;

namespace RadencyLibraryWebAPI.Models
{
	public class Rating
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		[Precision(10,2)]
		public decimal Score { get; set; }
		// Relationships
		public Book? Book { get; set; }
	}
}
