using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class RatingIdProfile : Profile
	{
		public RatingIdProfile() {
			CreateMap<Rating, RatingIdDto>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					);
		}
	}
}
