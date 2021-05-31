using API.Dto;
using Infrastructure.Erros;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		private readonly IGenericRepository<Vacancy> _vacancyRepository;
		private readonly IGenericRepository<VacancyApplications> _vacanciesApplications;
		private readonly IGenericRepository<BusinessWorker> _businessWorkerRepository;
		private readonly UserManager<User> _userManager;

		public BusinessPresentation(
			IMapper mapper,
			IBusinessService businessService,
			IGenericRepository<Business> businessRepository,
			IGenericRepository<Vacancy> vacancyRepository,
			IGenericRepository<VacancyApplications> vacanciesApplications,
			IGenericRepository<BusinessWorker> businessWorkerRepository,
			UserManager<User> userManager)
		{
			_mapper = mapper;
			_businessService = businessService;
			_businessRepository = businessRepository;
			_vacancyRepository = vacancyRepository;
			_vacanciesApplications = vacanciesApplications;
			_userManager = userManager;
			_businessWorkerRepository = businessWorkerRepository;
		}

		public BusinessPresentation()
		{
		}

		public async Task<ActionResult<ApiResponse>> AcceptBusinessRequest(int businessId)
		{
			Business business = await _businessRepository.GetByIdAsync(businessId);

			if (!await _businessService.AcceptBusinessRequest(business))
			{
				return new BadRequestObjectResult(new ApiResponse(400, "Cannot accept this business"));
			};

			User user = await _userManager.FindByIdAsync(business.OwnerId);

			if (await _userManager.IsInRoleAsync(user, "BusinessOwner"))
				return new OkObjectResult(new ApiResponse(200, "You accepted business request"));

			IdentityResult addRoleResult = await _userManager.AddToRoleAsync(user, "BusinessOwner");

			return addRoleResult.Succeeded
				? new OkObjectResult(new ApiResponse(200, "You accepted business request"))
				: new BadRequestObjectResult(new ApiResponse(400, "Cannot accept this business"));
		}

		public async Task<ActionResult<ApiResponse>> CreateBusinessRequest(BusinessDto businessDto)
		{
			Business business = _mapper.Map<BusinessDto, Business>(businessDto);
			User user = await _userManager.FindByIdAsync(business.OwnerId);

			if (!_businessService.IsHasMoneyForOpenBusiness(user, business.MaxCountOfWorker))
			{
				return new BadRequestObjectResult(new ApiResponse(400, "You need more money, for open this business."));
			}

			if (await _businessService.CreateBusinessRequest(business))
			{
				return new OkObjectResult(new ApiResponse(200, "Request status is pending. Please waiting"));
			}

			return new BadRequestObjectResult(new ApiResponse(400, "Somthing wrong. Try later"));
		}

		public async Task<bool> CreateVacancy(BusinessVacancyDto businessVacancyDto)
		{
			Vacancy vacancy = _mapper.Map<BusinessVacancyDto, Vacancy>(businessVacancyDto);

			return await _businessService.CreateVacancyForBusiness(vacancy);
		}

		public async Task<TableData<WorkerDto>> GetBusinessWorkers(TableParams tableParams, int businessId)
		{
			CountBusinessWorkersForBusinessSpecification countData = new CountBusinessWorkersForBusinessSpecification(businessId);
			ListBusinessWorkersForBusinessSpecification dataSpec = new ListBusinessWorkersForBusinessSpecification(tableParams, businessId);

			int count = await _businessWorkerRepository.CountAsync(countData);

			IReadOnlyList<BusinessWorker> businessWorkers = await _businessWorkerRepository.ListAsync(dataSpec);
			List<WorkerDto> data = await SetBusinessWorkersToWorkersDto(businessWorkers);

			return new TableData<WorkerDto> { Data = data, Count = count };
		}

		private async Task<List<WorkerDto>> SetBusinessWorkersToWorkersDto(IReadOnlyList<BusinessWorker> businessWorkers)
		{
			List<WorkerDto> workers = new List<WorkerDto>();

			foreach (BusinessWorker worker in businessWorkers)
			{
				WorkerDto businessWorker = _mapper.Map<BusinessWorker, WorkerDto>(worker);

				User user = await _userManager.FindByIdAsync(worker.WorkerId);

				businessWorker.FirstName = user.FirstName;
				businessWorker.LastName = user.LastName;

				workers.Add(businessWorker);
			}

			return workers;
		}

		public async Task<TableData<VacancyRespondDto>> GetAllBusinessVacanciesRespond(TableParams tableParams, int businessId)
		{
			CountBusinessVacancyRespondSpecification countData = new CountBusinessVacancyRespondSpecification(businessId);
			BusinessVacancyRespondSpecification dataSpec = new BusinessVacancyRespondSpecification(tableParams, businessId);

			int count = await _vacanciesApplications.CountAsync(countData);

			IReadOnlyList<VacancyApplications> vacancyApplications = await _vacanciesApplications.ListAsync(dataSpec);
			List<VacancyRespondDto> vacancyResponds = await SetUserToVacancy(vacancyApplications);

			return new TableData<VacancyRespondDto> { Data = vacancyResponds, Count = count };
		}

		private async Task<List<VacancyRespondDto>> SetUserToVacancy(IReadOnlyList<VacancyApplications> vacancyApplications)
		{
			List<VacancyRespondDto> vacancyResponds = new List<VacancyRespondDto>();

			foreach (VacancyApplications vacancyApplication in vacancyApplications)
			{
				VacancyRespondDto vacancyRespond = _mapper.Map<VacancyApplications, VacancyRespondDto>(vacancyApplication);

				User user = await _userManager.FindByIdAsync(vacancyApplication.ApplicantId);

				vacancyRespond.FirstName = user.FirstName;
				vacancyRespond.LastName = user.LastName;

				vacancyResponds.Add(vacancyRespond);
			}

			return vacancyResponds;
		}

		public async Task<Pagination<FullVacancyDto>> GetAllVacancies(BaseSpecParams specParams, ClaimsPrincipal user)
		{
			CountAllVacanciesSpecification countData = new CountAllVacanciesSpecification();
			ListAllVacanciesSpecification dataSpec = new ListAllVacanciesSpecification(specParams);

			int count = await _vacancyRepository.CountAsync(countData);

			IReadOnlyList<Vacancy> data = await _vacancyRepository.ListAsync(dataSpec);
			IReadOnlyList<FullVacancyDto> result = _mapper.Map<IReadOnlyList<Vacancy>, IReadOnlyList<FullVacancyDto>>(data);


			return new Pagination<FullVacancyDto>(specParams.PageIndex, specParams.PageSize, count, result);
		}

		public async Task<TableData<BusinessDto>> GetBusinessRequests(TableParams tableParams)
		{
			CountBusinessRequestSpecification countData = new CountBusinessRequestSpecification();
			TableDataBusinessRequestSpecification dataSpec = new TableDataBusinessRequestSpecification(tableParams);

			int count = await _businessRepository.CountAsync(countData);

			IReadOnlyList<Business> data = await _businessRepository.ListAsync(dataSpec);
			IReadOnlyList<BusinessDto> result = _mapper.Map<IReadOnlyList<Business>, IReadOnlyList<BusinessDto>>(data);

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

		public async Task<bool> RespondVacancy(int vacancyId, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			VacancyApplications vacancyApplications = new VacancyApplications
			{
				VacancyId = vacancyId,
				VacancyStatus = VacancyStatus.Pending,
				ApplicantId = user.Id
			};

			return await _businessService.RespondVacancy(vacancyApplications);
		}

		public async Task<TableData<UserRespondVacancyDto>> GetUserRespondVacancies(TableParams tableParams, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			CountUserRespondApplicationsSpecification countData = new CountUserRespondApplicationsSpecification(user.Id);
			ListUserRespondVacanciesSpecification dataSpec = new ListUserRespondVacanciesSpecification(tableParams, user.Id);

			int totalCount = await _vacanciesApplications.CountAsync(countData);

			IReadOnlyList<VacancyApplications> data = await _vacanciesApplications.ListAsync(dataSpec);
			IReadOnlyList<UserRespondVacancyDto> result = _mapper.Map<IReadOnlyList<VacancyApplications>, IReadOnlyList<UserRespondVacancyDto>>(data);

			return new TableData<UserRespondVacancyDto> { Data = result, Count = totalCount };
		}

		public async Task<bool> AcceptWorker(int vacancyApplicationId)
		{
			VacancyApplications vacancy = await _vacanciesApplications.GetAll()
																	  .Include(va => va.Vacancy)
																	  .FirstOrDefaultAsync(va => va.Id == vacancyApplicationId);

			return await _businessService.AcceptWorker(vacancy);
		}

		public async Task<bool> RejectVacancyRespond(int id)
		{
			VacancyApplications vacancy = await _vacanciesApplications.GetByIdAsync(id);

			return await _businessService.RejectVacancyRespond(vacancy);
		}

		public async Task<ActionResult<bool>> DismissWoker(int id)
		{
			BusinessWorker businessWorker = await _businessWorkerRepository.GetByIdAsync(id);

			return await _businessService.DismissWorker(businessWorker);
		}
	}
}
