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
		public BooksController(ILogger<BooksController> logger, IMapper mapper, LibraryDbContext context)
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
		public async Task<ActionResult> GetBooksOrderedByTitleAuthor(string? order) //<IEnumerable<BookCompactDto>>
		{
			try
			{
				List<Book> books = await _context.Books
				.Include(b => b.Reviews)
				.Include(b => b.Ratings)
				.ToListAsync();

				List<BookCompactDto> booksMappedToDto = _mapper.Map<List<Book>, List<BookCompactDto>>(books);

				if (order != null)
				{
					switch (order.ToLower())
					{
						case "author": return Ok(booksMappedToDto.OrderBy(b => b.Author).ToList());
						case "title": return Ok(booksMappedToDto.OrderBy(b => b.Title).ToList());
						default: return StatusCode(StatusCodes.Status404NotFound, "Wrong order option");
					}
				}
				else
				{
					return Ok(booksMappedToDto);
				}
			}
			catch (Exception ex)
			{
				return ReportError(
					StatusCodes.Status500InternalServerError, exception: ex);
			}
		}
		/*
		3. Get book details with the list of reviews
		GET https://{{baseUrl}}/api/books/{id}
		*/
		[HttpGet("{id:int}", Name = "GetBookById")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var book = await _context.Books
				.Include(b => b.Reviews)
				.Include(b => b.Ratings)
				.FirstOrDefaultAsync(b => b.Id == id);
			if (book != null)
			{
				BookDetailedDto bookDetailedDto = _mapper.Map<BookDetailedDto>(book);
				return Ok(bookDetailedDto);
			}
			else
			{
				return ReportError(
					StatusCodes.Status404NotFound,
					$"Book not found (BookId:{id})");
			}
		}
		/*
		4. Delete a book using a secret key. Save the secret key in the config of your application. Compare this key with a query param
		DELETE https://{{baseUrl}}/api/books/{id}?secret=qwerty
		*/
		[HttpDelete("{id:int}", Name = "DeleteBookById")]
		public async Task<IActionResult> DeleteBookById(int id, string? secret)
		{
			string _secret = Environment.GetEnvironmentVariable("HTTPDELETE_SECRET") ?? "";
			if (secret != null && secret != "")
			{
				if (_secret == secret)
				{
					try
					{
						var books = _context.Books;
						var bookToDelete = await books.FirstOrDefaultAsync(b => b.Id == id);
						var bookDeletedDto = _mapper.Map
							<BookIdDto>
							(bookToDelete);
						books.Remove(bookToDelete!);
						// Removing associated Reviews
						_context.Reviews.RemoveRange(
							_context.Reviews.Where(r => r.BookId == id));
						// Removing associated Ratings
						_context.Ratings.RemoveRange(
							_context.Ratings.Where(r => r.BookId == id));
						// Applying changes
						await _context.SaveChangesAsync();

						return Ok(bookDeletedDto);
					}
					catch (Exception ex)
					{
						return ReportError(StatusCodes.Status500InternalServerError, exception: ex);
					}
				}
				else return ReportError(
					StatusCodes.Status405MethodNotAllowed,
					"Wrong secret key");
			}
			else
			{

				return ReportError(
					StatusCodes.Status403Forbidden,
					"Secret key must be provided");
			}
		}
		/*
		5. Save a new book.
		POST https://{{baseUrl}}/api/books/save
		*/
		[HttpPost("save", Name = "PostBookSave")]
		public async Task<IActionResult> PostBookSave(BookNewDto book)
		{
			try
			{
				if (book == null)
					return ReportError(StatusCodes.Status400BadRequest, "Unable to create or update book");
				Book newBookConverted = _mapper.Map<Book>(book);
				var bookExists = await _context.Books.FindAsync(newBookConverted.Id);
				BookIdDto bookSavedDto;
				if (bookExists == null)
				{
					// Adding new book if book not found by Id
					await _context.Books.AddAsync(newBookConverted);
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
				await _context.SaveChangesAsync();
				return Ok(bookSavedDto);
			}
			catch (Exception ex)
			{
				return ReportError(
					StatusCodes.Status500InternalServerError,
					"Unable to create or update book", ex);
			}
		}
		/*
		6. Save a review for the book.
		PUT https://{{baseUrl}}/api/books/{id}/review
		*/
		[HttpPut("{id:int}/review", Name = "PutReviewById")]
		public async Task<IActionResult> PutReviewById(int id, ReviewNewDto review)
		{
			try
			{
				if (review == null)
					return ReportError(StatusCodes.Status400BadRequest, $"Unable to leave review about book (BookId:{id})");
				Review newReviewConverted = _mapper.Map<Review>(review);
				newReviewConverted.BookId = id;
				await _context.Reviews.AddAsync(newReviewConverted);
				await _context.SaveChangesAsync();
				ReviewIdDto resultReview = _mapper.Map<ReviewIdDto>(newReviewConverted);
				return Ok(resultReview);
			}
			catch (Exception ex)
			{
				return ReportError(
					StatusCodes.Status500InternalServerError,
					$"Unable to publish review of book (BookId:{id})", ex);
			}
		}
		/*
		7. Rate a book
		PUT https://{{baseUrl}}/api/books/{id}/rate
		*/
		[HttpPut("{id:int}/rate", Name = "PutRateById")]
		public async Task<IActionResult> PutRateById(int id, RatingNewDto rating)
		{
			try
			{
				if (rating == null)
					return ReportError(StatusCodes.Status400BadRequest, $"Unable to rate book {id}");
				Rating newRatingConverted = _mapper.Map<Rating>(rating);
				newRatingConverted.BookId = id;
				await _context.Ratings.AddAsync(newRatingConverted);
				await _context.SaveChangesAsync();
				RatingIdDto resultRating = _mapper.Map<RatingIdDto>(newRatingConverted);
				return Ok(resultRating);
			}
			catch (Exception ex)
			{
				return ReportError(
					StatusCodes.Status500InternalServerError,
					$"Unable to publish rating of book BookId:{id}", ex);
			}
		}
		private ObjectResult ReportError(int statusCode, string reportMessage = "", Exception? exception = null)
		{
			if (exception != null)
			{
				_logger.LogError(String.Join(" | ",
				DateTime.UtcNow.ToShortDateString(),
				statusCode.ToString(),
				reportMessage,
				exception.Message,
				exception.StackTrace));
			}
			else
			{
				_logger.LogError(String.Join(" | ",
				DateTime.UtcNow.ToShortDateString(),
				statusCode.ToString(),
				reportMessage,
				"There is no exception"));
			}
			return StatusCode(statusCode, reportMessage);
		}
	}
}
