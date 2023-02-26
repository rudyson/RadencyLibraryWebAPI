using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BooksController : Controller
	{
		
		private readonly ILogger<BooksController> _logger;
		private readonly LibraryDbContext _context;
		private readonly IMapper _mapper;
		public BooksController(ILogger<BooksController> logger,IMapper mapper, LibraryDbContext context)
		{
			DotNetEnv.Env.Load();
			_logger = logger;
			_context = context; 
			_mapper = mapper;
		}
		/*
		1. Get all books. Order by provided value (title or author)
		GET https://{{baseUrl}}/api/books?order=author
		*/
		[HttpGet(Name = "GetBooksOrderedByTitleAuthor")]
		public ActionResult GetBooksOrderedByTitleAuthor(string order) //<IEnumerable<BookCompactDto>>
		{
			try
			{
				List<Book> books= _context.Books
				.Include(b => b.Reviews)
				.Include(b => b.Ratings)
				.ToList();

				List<BookCompactDto> booksMappedToDto = _mapper.Map<List<Book>,List<BookCompactDto>>(books);

				switch (order.ToLower())
				{
					case "author": return Ok(booksMappedToDto.OrderBy(b => b.Author).ToList());
					case "title": return Ok(booksMappedToDto.OrderBy(b => b.Title).ToList());
					default: return StatusCode(404, "Wrong order option");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		/*
		3. Get book details with the list of reviews
		GET https://{{baseUrl}}/api/books/{id}
		*/
		[HttpGet("{id}", Name = "GetBookById")]
		public IActionResult GetBookById(int id)
		{
			var book = _context.Books
				.Include(b => b.Reviews)
				.Include(b => b.Ratings)
				.FirstOrDefault(b => b.Id == id);
			if (book != null)
			{
				BookDetailedDto bookDetailedDto = _mapper.Map<BookDetailedDto>(book); 
				return Ok(bookDetailedDto);
			}
			else
			{
				return StatusCode(404, "Book not found");
			}
		}
		/*
		4. Delete a book using a secret key. Save the secret key in the config of your application. Compare this key with a query param
		DELETE https://{{baseUrl}}/api/books/{id}?secret=qwerty
		*/
		[HttpDelete(Name = "DeleteBookById")]
		public IActionResult DeleteBookById(int id, string secret)
		{
			string _secret = Environment.GetEnvironmentVariable("HTTPDELETE_SECRET") ?? "";
			if (_secret == secret)
			{
				try
				{
					var books = _context.Books;
					var bookToDelete = books.FirstOrDefault(b => b.Id == id);
					var bookDeletedDto = _mapper.Map
						<BookIdDto>
						(bookToDelete);
					books.Remove(bookToDelete!);
					_context.SaveChanges();

					return Ok(bookDeletedDto);
				}
				catch (Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
			}
			else return StatusCode(404, "Wrong secret");
		}
		/*
		5. Save a new book.
		POST https://{{baseUrl}}/api/books/save
		*/
		[HttpPost("save",Name = "PostBookSave")]
		public IActionResult PostBookSave(BookNewDto book)
		{
			try
			{
				if (book == null)
					return BadRequest();
				Book newBookConverted = _mapper.Map<Book>(book);
				var bookExists = _context.Books.Find(newBookConverted.Id);
				BookIdDto bookSavedDto;
				if (bookExists == null)
				{
					// Adding new book if book not found by Id
					_context.Books.Add(newBookConverted);
					bookSavedDto = _mapper.Map<BookIdDto>(newBookConverted);
				}
				else
				{
					// Updating existing book information if found by Id
					if (newBookConverted.Title != null)
						bookExists.Title = newBookConverted.Title;
					if (newBookConverted.Cover != null)
						bookExists.Cover = newBookConverted.Cover;
					if (newBookConverted.Content != null)
						bookExists.Content = newBookConverted.Content;
					if (newBookConverted.Genre != null)
						bookExists.Genre = newBookConverted.Genre;
					if (newBookConverted.Author != null)
						bookExists.Author = newBookConverted.Author;
					bookSavedDto = _mapper.Map<BookIdDto>(bookExists);
				}
				_context.SaveChanges();
				
				return Ok(bookSavedDto);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Unable to create new book");
			}
		}
		/*
		6. Save a review for the book.
		PUT https://{{baseUrl}}/api/books/{id}/review
		*/
		[HttpPut("{id}/review", Name = "PutReviewById")]
		public IActionResult PutReviewById(int id,ReviewNewDto review) {
			try
			{
				if (review == null)
					return BadRequest();
				Review newReviewConverted = _mapper.Map<Review>(review);
				newReviewConverted.BookId = id;
				_context.Reviews.Add(newReviewConverted);
				_context.SaveChanges();
				ReviewIdDto resultReview = _mapper.Map<ReviewIdDto>(newReviewConverted);
				return Ok(resultReview);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, " | ", ex.StackTrace);
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Unable to publish review of book BookId:{id}");
			}
		}
		/*
		7. Rate a book
		PUT https://{{baseUrl}}/api/books/{id}/rate
		*/
		[HttpPut("{id}/rate", Name = "PutRateById")]
		public IActionResult PutRateById(int id, RatingNewDto rating)
		{
			try
			{
				if (rating == null)
					return BadRequest();
				Rating newRatingConverted = _mapper.Map<Rating>(rating);
				newRatingConverted.BookId = id;
				_context.Ratings.Add(newRatingConverted);
				_context.SaveChanges();
				RatingIdDto resultRating = _mapper.Map<RatingIdDto>(newRatingConverted);
				return Ok(resultRating);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message," | ",ex.StackTrace);
				return StatusCode(StatusCodes.Status500InternalServerError,
					$"Unable to publish rating of book BookId:{id}");
			}
		}
	}
}
