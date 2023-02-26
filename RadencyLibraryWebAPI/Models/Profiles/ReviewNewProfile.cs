using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class ReviewNewProfile : Profile
	{
		public ReviewNewProfile()
		{
			CreateMap<ReviewNewDto, Review>()
				.ForMember(
					dest => dest.Message,
					opt => opt.MapFrom(src => src.Message)
					)
				.ForMember(
					dest => dest.Reviewer,
					opt => opt.MapFrom(src => src.Reviewer)
					);
		}
	}
}
