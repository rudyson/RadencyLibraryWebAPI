using Microsoft.AspNetCore.Mvc;

namespace RadencyLibraryWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecommendedController : Controller
	{
		private readonly ILogger<RecommendedController> _logger;
		public RecommendedController(ILogger<RecommendedController> logger)
		{
			_logger = logger;
		}
		/*
		2. Get top 10 books with high rating and number of reviews greater than 10. You can filter books by specifying genre. Order by rating
		GET https://{{baseUrl}}/api/recommended?genre=horror
		*/

		[HttpGet(Name = "GetRecommended")]
		public IActionResult Index()
		{
			_logger.LogDebug("Visited RecommendedControler/GetRecommended");
			return Content("Empty RecommendedControler");
		}
	}
}
