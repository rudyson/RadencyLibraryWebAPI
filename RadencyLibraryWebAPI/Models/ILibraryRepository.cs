using RadencyLibraryWebAPI.Entities;
namespace RadencyLibraryWebAPI.Models
{
	public interface ILibraryRepository
	{
		IEnumerable<Book> Books { get;}
		IEnumerable<Rating> Ratings { get;}
		IEnumerable<Review> Reviews { get;}
	}
}
