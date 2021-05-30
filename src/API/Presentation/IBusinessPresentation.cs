using API.Dto;
using Infrastructure.Erros;
using API.Helpers;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IBusinessPresentation
    {
        Task<ActionResult<ApiResponse>> CreateBusinessRequest(BusinessDto businessDto);
		Task<ActionResult<ApiResponse>> AcceptBusinessRequest(int businessId);
		Task<bool> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto);
		Task<bool> CreateVacancy(BusinessVacancyDto businessVacancyDto);
		Task<bool> RespondVacancy(int vacancyId, ClaimsPrincipal user);
		Task<TableData<BusinessDto>> GetBusinessRequests(TableParams tableParams);
		Task<Pagination<BusinessDto>> GetUserBusiness(BaseSpecParams specParams, ClaimsPrincipal claims);
		Task<TableData<BusinessDto>> GetPendingBuisness(TableParams tableParams, ClaimsPrincipal claims);
		Task<Pagination<FullVacancyDto>> GetAllVacancies(BaseSpecParams tableParams, ClaimsPrincipal user);
		Task<TableData<VacancyRespondDto>> GetAllBusinessVacanciesRespond(TableParams tableParams, int businessId);
		Task<TableData<UserRespondVacancyDto>> GetUserRespondVacancies(TableParams tableParams, ClaimsPrincipal user);
		Task<TableData<WorkerDto>> GetBusinessWorkers(TableParams tableParams, int businessId);
		Task<bool> AcceptWorker(int vacancyApplicationId);
		Task<bool> RejectVacancyRespond(int id);
		Task<ActionResult<bool>> DismissWoker(int id);
	}
}
