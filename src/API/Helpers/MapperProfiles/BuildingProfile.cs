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

			CreateMap<UserAppartament, AppartamentViewDto>()
				.ForMember(up => up.Id, opt => opt.MapFrom(a => a.Appartament.Id))
				.ForMember(up => up.Cost, opt => opt.MapFrom(a => a.Appartament.Cost))
				.ForMember(up => up.CountRooms, opt => opt.MapFrom(a => a.Appartament.CountRooms))
				.ForMember(up => up.Floor, opt => opt.MapFrom(a => a.Appartament.Floor))
				.ForMember(up => up.TypeAppartament, opt => opt.MapFrom(a => a.Appartament.TypeAppartament))
				.ForMember(up => up.Description, opt => opt.MapFrom(a => a.Appartament.PictureUrl))
				.ForMember(up => up.Title, opt => opt.MapFrom(a => a.Appartament.Title))
				.ForMember(up => up.PictureUrl, opt => opt.MapFrom(a => a.Appartament.PictureUrl));
		}
	}
}
