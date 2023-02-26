using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class RatingNewProfile : Profile
	{
		public RatingNewProfile()
		{
			CreateMap<RatingNewDto,Rating>()
				.ForMember(
					dest => dest.Score,
					opt => opt.MapFrom(src => src.Score)
					);
		}
	}
}
