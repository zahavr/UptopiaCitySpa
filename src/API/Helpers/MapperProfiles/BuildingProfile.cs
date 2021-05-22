using API.Dto;
using API.Dto.BuildingDto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers.MapperProfiles
{
	public class BuildingProfile : Profile
	{
		public BuildingProfile()
		{
			CreateMap<BuildingDto, Building>()
				.ReverseMap();

			CreateMap<AppartamentDto, Appartament>()
				.ReverseMap();

			CreateMap<Appartament, AppartamentViewDto>()
				.ReverseMap();

			CreateMap<UserAppartament, AppartamentForPoliceDto>()
				.ForMember(ap => ap.Title, opt => opt.MapFrom(ua => ua.Appartament.Title))
				.ForMember(ap => ap.Cost, opt => opt.MapFrom(ua => ua.Appartament.Cost))
				.ForMember(ap => ap.TypeAppartament, opt => opt.MapFrom(ua => ua.Appartament.TypeAppartament))
				.ForMember(ap => ap.CountRooms, opt => opt.MapFrom(ua => ua.Appartament.CountRooms));
		}
	}
}
