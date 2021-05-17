using API.Dto.BusinessDto;
using API.Errors;
using API.Helpers;
using API.Presentation;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class BusinessController : BaseController
	{
		private readonly IBusinessPresentation _businessPresentation;

		public BusinessController(
			IBusinessPresentation businessPresentation)
		{
			_businessPresentation = businessPresentation;
		}

		[Authorize(Roles = "CityManager")]
		[HttpGet("get-business-requests")]
		public async Task<TableData<BusinessDto>> GetBusinessRequests([FromQuery] TableParams tableParams)
		{
			return await _businessPresentation.GetBusinessRequests(tableParams);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpGet("get-my-business")]
		public async Task<ActionResult<Pagination<BusinessDto>>> GetUserBusiness([FromQuery] BaseSpecParams specParams)
		{
			return await _businessPresentation.GetUserBusiness(specParams, HttpContext.User);
		}

		[Authorize]
		[HttpGet("user-business-applications")]
		public async Task<TableData<BusinessDto>> GetPendingBusiness([FromQuery] TableParams tableParams)
		{
			return await _businessPresentation.GetPendingBuisness(tableParams, HttpContext.User);
		}

		[Authorize]
		[HttpPost("create-business-request")]
		public async Task<ActionResult<ApiResponse>> CreateBusinessRequest(BusinessDto businessDto)
		{
			return await _businessPresentation.CreateBusinessRequest(businessDto);
		}

		[Authorize(Roles = "CityManager")]
		[HttpPatch("accept-business-request/{businessId}")]
		public async Task<ActionResult<ApiResponse>> AcceptBusinessRequest(int businessId)
		{
			return await _businessPresentation.AcceptBusinessRequest(businessId);
		}

		[Authorize(Roles = "CityManager")]
		[HttpPost("reject-business-application")]
		public async Task<ActionResult<bool>> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto)
		{
			return await _businessPresentation.RejectBusinessRequest(rejectApplicationDto);
		}

		[Authorize]
		[HttpGet("get-all-vacancy")]
		public async Task<Pagination<FullVacancyDto>> GetAllVacancies([FromQuery] BaseSpecParams specParams)
		{
			return await _businessPresentation.GetAllVacancies(specParams, HttpContext.User);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpPost("create-vacansy")]
		public async Task<ActionResult<bool>> CreateVacancy(BusinessVacancyDto businessVacancyDto)
		{
			return await _businessPresentation.CreateVacancy(businessVacancyDto);
		}

		[Authorize]
		[HttpPost("respond-vacancy/{vacancyId}")]
		public async Task<ActionResult<bool>> RespondVacancy(int vacancyId)
		{
			return await _businessPresentation.RespondVacancy(vacancyId, HttpContext.User);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpGet("get-vacancies-respond/{businessId}")]
		public async Task<ActionResult<TableData<VacancyRespondDto>>> GetRespondVacanciesForBusiness([FromQuery] TableParams tableParams, int businessId)
		{
			return await _businessPresentation.GetAllBusinessVacanciesRespond(tableParams, businessId);
		}

		[Authorize]
		[HttpGet("get-user-vacancies")]
		public async Task<ActionResult<TableData<UserRespondVacancyDto>>> GetUserVacancies([FromQuery] TableParams tableParams)
		{
			return await _businessPresentation.GetUserRespondVacancies(tableParams, HttpContext.User);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpGet("get-workers/{businessId}")]
		public async Task<ActionResult<TableData<WorkerDto>>> GetBusinessWorkers([FromQuery] TableParams tableParams, int businessId)
		{
			return await _businessPresentation.GetBusinessWorkers(tableParams, businessId);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpPatch("accept-worker/{vacancyApplicationId}")]
		public async Task<ActionResult<bool>> AcceptWorker(int vacancyApplicationId)
		{
			return await _businessPresentation.AcceptWorker(vacancyApplicationId);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpDelete("dismiss-worker/{id}")]
		public async Task<ActionResult<bool>> DismissWoker(int id)
		{
			return await _businessPresentation.DismissWoker(id);
		}
	}
}
