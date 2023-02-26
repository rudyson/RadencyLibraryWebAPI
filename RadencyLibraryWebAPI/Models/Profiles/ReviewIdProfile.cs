using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class ReviewIdProfile : Profile
	{
		public ReviewIdProfile()
		{
			CreateMap<Review, ReviewIdDto>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					);
		}
	}
}
