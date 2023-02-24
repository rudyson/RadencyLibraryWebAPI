using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RadencyLibraryWebAPI.Models;

namespace RadencyLibraryWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BooksController : Controller
	{
		private readonly ILogger<BooksController> _logger;
		private readonly LibraryDbContext _context;
		private readonly IMapper _mapper;
		public BooksController(ILogger<BooksController> logger, LibraryDbContext context,IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}
		/*
		1. Get all books. Order by provided value (title or author)
		GET https://{{baseUrl}}/api/books?order=author
		*/
		/*
		[HttpGet(Name = "GetBooksOrderedByTitleAuthor")]
		public ActionResult<IEnumerable<Book>> GetBooks(string order)
		{
			try
			{
				var books = _context.Books.Include(b => b.Reviews).Include(b => b.Ratings).ToList();
				switch (order)
				{
					case "author" : return Ok(books.OrderBy(b => b.Author).ToList());
					case "title": return Ok(books.OrderBy(b => b.Title).ToList());
					default: return StatusCode(404, "Wrong order option");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		*/
		/*
		3. Get book details with the list of reviews
		GET https://{{baseUrl}}/api/books/{id}
		*/
		[HttpGet("{id}", Name = "GetBookById")]
/*
 Ratings null ?; - Reviews=>Book; BookId, Id
 */
		public IActionResult Get(int id)
		{
			var book = _context.Books
				//.Include(b => b.Reviews)
				.FirstOrDefault(b => b.Id == id);
			if (book == null) return StatusCode(404, "Book not found");
			else return Ok(book);
		}
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

		[HttpGet(Name = "Index")]
		public IActionResult Index()
		{
			//BooksController/GetBooks

			_logger.LogDebug(String.Join("/",
				this.ControllerContext.RouteData.Values["controller"],
				this.ControllerContext.RouteData.Values["action"]));
			//_logger.LogInformation(String.Join("\n", this.ControllerContext.RouteData.Values.Values));
			return Content("Empty BooksController");
		}
	}
}
