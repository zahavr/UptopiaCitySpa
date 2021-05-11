using API.Dto;
using API.Dto.BusinessDto;
using API.Presentation;
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

		[Authorize]
		[HttpPost("create-business-request")]
		public async Task<ActionResult<bool>> CreateBusinessRequest(BusinessDto businessDto)
		{
			return await _businessPresentation.CreateBusinessRequest(businessDto);
		}

		[Authorize]
		[HttpPatch("accept-business-request")]
		public async Task<ActionResult<bool>> AcceptBusinessRequest(AcceptBusinessDto acceptBusinessDto)
		{
			return await _businessPresentation.AcceptBusinessRequest(acceptBusinessDto);
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
	}
}
