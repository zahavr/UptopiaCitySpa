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
		}
	}
}
