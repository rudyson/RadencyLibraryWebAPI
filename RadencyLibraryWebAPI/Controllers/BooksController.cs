using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace RadencyLibraryWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BooksController : Controller
	{
		/*
		1. Get all books. Order by provided value (title or author)
		GET https://{{baseUrl}}/api/books?order=author
		*/

		/*
		3. Get book details with the list of reviews
		GET https://{{baseUrl}}/api/books/{id}
		*/

		/*
		4. Delete a book using a secret key. Save the secret key in the config of your application. Compare this key with a query param
		DELETE https://{{baseUrl}}/api/books/{id}?secret=qwerty
		*/

		/*
		5. Save a new book.
		POST https://{{baseUrl}}/api/books/save
		*/

		/*
		6. Save a review for the book.
		PUT https://{{baseUrl}}/api/books/{id}/review
		*/

		/*
		7. Rate a book
		PUT https://{{baseUrl}}/api/books/{id}/rate
		*/

		[HttpGet(Name = "GetBooks")]
		public IActionResult Index()
		{
			return Content("Empty BooksController");
		}
	}
}
