using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helpers.MapperProfiles
{
	public class PoliceProfile : Profile
    {
		public PoliceProfile()
		{
			CreateMap<ViolationDto, Violation>();
			CreateMap<Violation, ViolationViewDto>();
		}
    }
}
