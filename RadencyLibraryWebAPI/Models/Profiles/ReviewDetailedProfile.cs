using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class ReviewDetailedProfile : Profile
	{
		public ReviewDetailedProfile() {
			CreateMap<Review, ReviewDetailedDto>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					)
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
