using AutoMapper;
using RadencyLibraryWebAPI.Entities;
using RadencyLibraryWebAPI.Models.DTO;

namespace RadencyLibraryWebAPI.Models.Profiles
{
	public class BookNewProfile : Profile
	{
		public BookNewProfile() {
			CreateMap<BookNewDto, Book>()
				.ForMember(
					dest => dest.Id,
					opt => opt.MapFrom(src => src.Id)
					)
				.ForMember(
					dest => dest.Title,
					opt => opt.MapFrom(src => src.Title)
					)
				.ForMember(
					dest => dest.Cover,
					opt => opt.MapFrom(src => src.Cover)
					)
				.ForMember(
					dest => dest.Content,
					opt => opt.MapFrom(src => src.Content)
					)
				.ForMember(
					dest => dest.Genre,
					opt => opt.MapFrom(src => src.Genre)
					)
				.ForMember(
					dest => dest.Author,
					opt => opt.MapFrom(src => src.Author)
					);
		}
	}
}
