using API.Dto.BusinessDto;
using API.Helpers;
using API.Presentation;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

		[Authorize]
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

		[HttpGet("user-business-applications")]
		public async Task<TableData<BusinessDto>> GetPendingBusiness([FromQuery] TableParams tableParams)
		{
			return await _businessPresentation.GetPendingBuisness(tableParams, HttpContext.User);
		}

		[Authorize]
		[HttpPost("create-business-request")]
		public async Task<ActionResult<bool>> CreateBusinessRequest(BusinessDto businessDto)
		{
			return await _businessPresentation.CreateBusinessRequest(businessDto);
		}

		[Authorize]
		[HttpPatch("accept-business-request/{businessId}")]
		public async Task<ActionResult<bool>> AcceptBusinessRequest(int businessId)
		{
			return await _businessPresentation.AcceptBusinessRequest(businessId);
		}

		[Authorize]
		[HttpPost("reject-business-application")]
		public async Task<ActionResult<bool>> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto)
		{
			return await _businessPresentation.RejectBusinessRequest(rejectApplicationDto);
		}

		[Authorize(Roles = "BusinessOwner")]
		[HttpPost("create-vacansy")]
		public async Task<ActionResult<bool>> CreateVacansy(BusinessVacancyDto businessVacancyDto)
		{
			return await _businessPresentation.CreateVacansy(businessVacancyDto);
		}

		[Authorize]
		[HttpPost("respond-vacancy")]
		public async Task<ActionResult<bool>> RespondVacancy(RespondVacancyDto respondVacancyDto)
		{
			return await _businessPresentation.RespondVacancy(respondVacancyDto, HttpContext.User);
		}
	}
}
