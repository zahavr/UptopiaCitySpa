using API.Dto;
using API.Dto.BusinessDto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class BusinessPresentation : IBusinessPresentation
	{
		private readonly IMapper _mapper;
		private readonly IBusinessService _businessService;
		private readonly IGenericRepository<Business> _businessRepository;
		private readonly UserManager<User> _userManager;

		public BusinessPresentation(
			IMapper mapper,
			IBusinessService businessService,
			IGenericRepository<Business> businessRepository,
			UserManager<User> userManager)
		{
			_mapper = mapper;
			_businessService = businessService;
			_businessRepository = businessRepository;
			_userManager = userManager;
		}

		public async Task<bool> AcceptBusinessRequest(AcceptBusinessDto acceptBusinessDto)
		{
			Business business = await _businessRepository.GetByIdAsync(acceptBusinessDto.BusinessId);

			if (!await _businessService.AcceptBusinessRequest(business)) return false;

			User user = await _userManager.FindByIdAsync(acceptBusinessDto.UserId);
			IdentityResult addRoleResut = await _userManager.AddToRoleAsync(user, "BusinessOwner");

			return addRoleResut.Succeeded;
		}

		public async Task<bool> CreateBusinessRequest(BusinessDto businessDto)
		{
			Business business = _mapper.Map<BusinessDto, Business>(businessDto);
			User user = await _userManager.FindByIdAsync(business.OwnerId);

			if (_businessService.IsHasMoneyForOpenBusiness(user, business.MaxCountOfWorker))
			{
				return await _businessService.CreateBusinessRequest(business);
			}

			return false;
		}

		public async Task<bool> CreateVacansy(BusinessVacancyDto businessVacancyDto)
		{
			Vacancy vacancy = _mapper.Map<BusinessVacancyDto, Vacancy>(businessVacancyDto);

			return await _businessService.CreateVacansyForBusiness(vacancy);
		}

		public async Task<bool> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto)
		{
			Business business = await _businessRepository.GetByIdAsync(rejectApplicationDto.BusinessId);
			RejectedApplications rejectApllications = _mapper.Map<RejectApplicationDto, RejectedApplications>(rejectApplicationDto);

			return await _businessService.RejectBusinessRequest(business, rejectApllications);
		}
	}
}
