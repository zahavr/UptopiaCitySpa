using API.Dto.BusinessDto;
using API.Helpers.Resolvers;
using AutoMapper;
using Core.Entities;

namespace API.Helpers.MapperProfiles
{
	public class BusinessProfile : Profile
    {
		public BusinessProfile()
		{
			CreateMap<BusinessDto, Business>()
				.ForMember(x => x.OwnerId, opt => opt.MapFrom<BusinessOwnerResolver>())
				.ForMember(x => x.BusinessStatus, opt => opt.MapFrom(x => BusinessStatus.Pending));

			CreateMap<RejectApplicationDto, RejectedApplications>();
			CreateMap<BusinessVacancyDto, Vacancy>();
		}
    }
}
