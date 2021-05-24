using API.Dto.WorkDto;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using System;

namespace API.Helpers.MapperProfiles
{
	public class WorkProfile : Profile
    {
		public WorkProfile()
		{
			CreateMap<BusinessWorker, WorkViewDto>()
				.ForMember(bw => bw.CompanyAddress, opt => opt.MapFrom(b => b.Business.Address))
				.ForMember(bw => bw.CompanyName, opt => opt.MapFrom(b => b.Business.Name))
				.ForMember(bw => bw.WorkExperience, opt => opt.MapFrom(b => DateTime.Now.CalculateWorkExpirience(b.StartWork)));
		}
    }
}
