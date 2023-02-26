using AutoMapper;
using RadencyLibraryWebAPI.Models.DTO;
using RadencyLibraryWebAPI.Entities;
using System.Linq;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class BookCompactProfile : Profile
	{
		public BookCompactProfile()
		{
			CreateMap<Book, BookCompactDto>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					)
				.ForMember(
					dest => dest.Title,
					opt => opt.MapFrom(src => src.Title)
					)
				.ForMember(
					dest => dest.Author,
					opt => opt.MapFrom(src => src.Author)
					)
				.ForMember(
					dest => dest.Rating,
					opt => opt.MapFrom(src => src.Ratings!.Select(r => r.Score).DefaultIfEmpty(0).Average())
					)
				/*
				.ForMember(
					dest => dest.Rating,
					opt => opt.MapFrom(
						(src, dest) => {
						if ((src.Ratings != null) && (src.Ratings.Any()))
						{
							try
							{
								return src.Ratings.Average(r => r.Score);

							}
							catch (Exception ex)
							{
								return 0.0m;
							}
						}
						else return 0.0m;
					})
					)
				*/
				.ForMember(
					dest => dest.ReviewsNumber,
					opt => opt.MapFrom(src => (from r in src.Reviews select r).Count())
					);
		}
	}
}
