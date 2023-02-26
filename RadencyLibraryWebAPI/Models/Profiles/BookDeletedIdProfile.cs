using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class BookDeletedIdProfile : Profile
	{
		public BookDeletedIdProfile() {
			CreateMap<Book, BookIdDto>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					);
		}
	}
}
