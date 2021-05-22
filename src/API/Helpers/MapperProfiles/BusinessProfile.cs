using API.Dto;
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
			CreateMap<Business, BusinessDto>();

			CreateMap<RejectApplicationDto, RejectedApplications>();
			CreateMap<BusinessVacancyDto, Vacancy>();
			CreateMap<Vacancy, FullVacancyDto>()
				.ForMember(fv => fv.VacancyTitle, opt => opt.MapFrom(v => v.Title))
				.ForMember(fv => fv.VacancyDescription, opt => opt.MapFrom(v => v.Description))
				.ForMember(fv => fv.VacancyId, opt => opt.MapFrom(v => v.Id))
				.ForMember(fv => fv.BusinessDescription, opt => opt.MapFrom(v => v.Business.Description))
				.ForMember(fv => fv.BusinessTitle, opt => opt.MapFrom(v => v.Business.Name))
				.ForMember(fv => fv.Salary, opt => opt.MapFrom(v => v.Salary))
				.ForMember(fv => fv.Address, opt => opt.MapFrom(v => v.Business.Address));

			CreateMap<VacancyApplications, VacancyRespondDto>()
				.ForMember(vr => vr.VacancyTitle, opt => opt.MapFrom(va => va.Vacancy.Title));

			CreateMap<VacancyApplications, UserRespondVacancyDto>()
				.ForMember(ur => ur.Title, opt => opt.MapFrom(va => va.Vacancy.Title))
				.ForMember(ur => ur.Description, opt => opt.MapFrom(va => va.Vacancy.Description))
				.ForMember(ur => ur.Salary, opt => opt.MapFrom(va => va.Vacancy.Salary));

			CreateMap<BusinessWorker, WorkerDto>();
		}
    }
}
