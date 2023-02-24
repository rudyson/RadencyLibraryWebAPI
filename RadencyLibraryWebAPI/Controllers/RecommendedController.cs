using Microsoft.AspNetCore.Mvc;

namespace RadencyLibraryWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecommendedController : Controller
	{
		/*
		2. Get top 10 books with high rating and number of reviews greater than 10. You can filter books by specifying genre. Order by rating
		GET https://{{baseUrl}}/api/recommended?genre=horror
		*/

		[HttpGet(Name = "GetRecommended")]
		public IActionResult Index()
		{
			return Content("Empty RecommendedControler");
		}
	}
}
