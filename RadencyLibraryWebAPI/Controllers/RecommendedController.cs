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
	public class RecommendedController : Controller
	{
		private readonly ILogger<RecommendedController> _logger;
		private readonly LibraryDbContext _context;
		private readonly IMapper _mapper;
		public RecommendedController(
			ILogger<RecommendedController> logger,
			IMapper mapper,
			LibraryDbContext context)
		{
			_logger = logger;
			_context = context;
			_mapper = mapper;
		}
		/*
		2. Get top 10 books with high rating and number of reviews greater than 10. You can filter books by specifying genre. Order by rating
		GET https://{{baseUrl}}/api/recommended?genre=horror
		*/
		[HttpGet(Name = "GetRecommendedBooksWithGenreFilter")]
		public async Task<ActionResult> GetRecommendedBooksWithGenreFilter(string? genre)
		{
			try
			{
				List<Book> books = await _context.Books
					.Include(b => b.Reviews)
					.Include(b => b.Ratings)
					.ToListAsync();
				if (genre != null)
				{
					
					List<Book> booksByGenre = books.Where(b => b.Genre!.ToLower() == genre.ToLower()).ToList();
					List<BookCompactDto> booksMappedToDto = _mapper.Map<List<Book>, List<BookCompactDto>>(booksByGenre);
					return Ok(
						booksMappedToDto
						.Where(x => x.ReviewsNumber >= 10)
						.OrderBy(x => x.Rating)
						.Take(10));
				}
				else {
					List<BookCompactDto> booksMappedToDto = _mapper.Map<List<Book>, List<BookCompactDto>>(books);
					return Ok(
						booksMappedToDto
						.Where(x => x.ReviewsNumber >= 10)
						.OrderBy(x => x.Rating)
						.Take(10));
				}
			}
			catch (Exception ex)
			{
				return ReportError(StatusCodes.Status500InternalServerError, exception: ex);
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
