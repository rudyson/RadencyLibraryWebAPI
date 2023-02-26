﻿using RadencyLibraryWebAPI.Entities;

namespace RadencyLibraryWebAPI.Models.DTO
{
	public class BookCompactDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Author { get; set; }
		public decimal? Rating { get; set; }
		public int ReviewsNumber { get; set; }
	}
}
