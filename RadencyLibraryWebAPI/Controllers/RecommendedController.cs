﻿using AutoMapper;
//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models;
using RadencyLibraryWebAPI.Models.DTO;
using System.Collections.Generic;

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
		[HttpGet("{genre?}",Name = "GetRecommendedBooksWithGenreFilter")]
		public ActionResult GetRecommendedBooksWithGenreFilter(string genre)
		{
			try
			{
				
				if (genre != null)
				{
					List<Book> books = _context.Books
					.Include(b => b.Reviews)
					.Include(b => b.Ratings)
					.ToList();
					List<Book> booksByGenre  = books.Where(b => b.Genre!.ToLower() == genre.ToLower()).ToList();
					List<BookCompactDto> booksMappedToDto = _mapper.Map<List<Book>, List<BookCompactDto>>(books);
					return Ok(
						booksMappedToDto
						.Where(x => x.ReviewsNumber > 10)
						.OrderBy(x => x.Rating)
						.Take(10));
				}
				else { return BadRequest(); }
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(500, ex.Message);
			}
		}
	}
}
