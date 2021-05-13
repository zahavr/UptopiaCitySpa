using API.Dto.BusinessDto;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
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

		public async Task<bool> AcceptBusinessRequest(int businessId)
		{
			Business business = await _businessRepository.GetByIdAsync(businessId);

			if (!await _businessService.AcceptBusinessRequest(business)) return false;

			User user = await _userManager.FindByIdAsync(business.OwnerId);

			if (await _userManager.IsInRoleAsync(user, "BusinessOwner"))
				return true;
		
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

		public async Task<TableData<BusinessDto>> GetBusinessRequests(TableParams tableParams)
		{
			CountBusinessRequestSpecification countData = new CountBusinessRequestSpecification();
			TableDataBusinessRequestSpecification dataSpec = new TableDataBusinessRequestSpecification(tableParams);

			int count = await _businessRepository.CountAsync(countData);

			IReadOnlyList<Business> data = await _businessRepository.ListAsync(dataSpec);
			IReadOnlyList<BusinessDto> result = _mapper.Map<IReadOnlyList<Business>, IReadOnlyList<BusinessDto>> (data);

			return new TableData<BusinessDto> { Data = result, Count = count };
		}

		public async Task<TableData<BusinessDto>> GetPendingBuisness(TableParams tableParams, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			CountUserPendingBusinessSpecification countData = new CountUserPendingBusinessSpecification(user.Id);
			UserPendingBusinessListSpecification dataSpec = new UserPendingBusinessListSpecification(tableParams, user.Id);

			int count = await _businessRepository.CountAsync(countData);

			IReadOnlyList<Business> data = await _businessRepository.ListAsync(dataSpec);
			IReadOnlyList<BusinessDto> result = _mapper.Map<IReadOnlyList<Business>, IReadOnlyList<BusinessDto>>(data);

			return new TableData<BusinessDto> { Data = result, Count = count };
		}

		public async Task<Pagination<BusinessDto>> GetUserBusiness(BaseSpecParams specParams, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			CountUserBusinessSpecififcation countData = new CountUserBusinessSpecififcation(user.Id);
			UserBusinessListSpecification dataSpec = new UserBusinessListSpecification(specParams, user.Id);

			int totalCount = await _businessRepository.CountAsync(countData);

			IReadOnlyList<Business> data = await _businessRepository.ListAsync(dataSpec);
			IReadOnlyList<BusinessDto> result = _mapper.Map<IReadOnlyList<Business>, IReadOnlyList<BusinessDto>>(data);

			return new Pagination<BusinessDto>(specParams.PageIndex, specParams.PageSize, totalCount, result);
		}

		public async Task<bool> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto)
		{
			Business business = await _businessRepository.GetByIdAsync(rejectApplicationDto.BusinessId);
			RejectedApplications rejectApllications = _mapper.Map<RejectApplicationDto, RejectedApplications>(rejectApplicationDto);

			rejectApllications.OwnerId = business.OwnerId;

			return await _businessService.RejectBusinessRequest(business, rejectApllications);
		}

		public async Task<bool> RespondVacancy(RespondVacancyDto respondVacancyDto, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			respondVacancyDto.ApplicantId = user.Id;
			respondVacancyDto.VacancyStatus = VacancyStatus.Pending;

			VacancyApplications vacancyApplications = _mapper.Map<RespondVacancyDto, VacancyApplications>(respondVacancyDto);

			return await _businessService.RespondVacancy(vacancyApplications);
		}
	}
}
