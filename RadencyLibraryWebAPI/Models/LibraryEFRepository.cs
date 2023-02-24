using RadencyLibraryWebAPI.Entities;

namespace RadencyLibraryWebAPI.Models
{
	public class LibraryEFRepository : ILibraryRepository
	{
		IEnumerable<Book> ILibraryRepository.Books => _context.Books;
		IEnumerable<Rating> ILibraryRepository.Ratings => _context.Ratings;
		IEnumerable<Review> ILibraryRepository.Reviews => _context.Reviews;
		private LibraryDbContext _context;
		public LibraryEFRepository(LibraryDbContext context)
		{
			_context = context;
		}
	}
}
