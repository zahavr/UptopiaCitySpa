using API.Dto;
using API.Presentation;
using Core.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Authorize(Policy = "Police")]
	public class PoliceController : BaseController
	{
		private readonly IPolicePresentation _policePresentation;

		public PoliceController(IPolicePresentation policePresentation)
		{
			_policePresentation = policePresentation;
		}

		[HttpPost("get-users")]
		public async Task<ActionResult<TableData<UserCabinetDto>>> GetUsers(TablePoliceParams tableParams)
		{
			return await _policePresentation.GetUsers(tableParams); ;
		}

		[HttpGet("get-user/{id}")]
		public async Task<ActionResult<FullUserInfo>> GetUser(string id)
		{
			return await _policePresentation.GetUser(id);
		}

		[HttpGet("get-user-appartaments/{userId}")]
		public async Task<IReadOnlyList<AppartamentForPoliceDto>> GetUserAppartaments(string userId)
		{
			return await _policePresentation.GetUserAppartaments(userId);
		}

		[HttpGet("get-user-business/{userId}")]
		public async Task<IReadOnlyList<BusinessDto>> GetUserBusinesses(string userId)
		{
			return await _policePresentation.GetUserBusiness(userId);
		}

		[HttpGet("get-user-friends/{userId}")]
		public async Task<IReadOnlyList<UserFriendViewDto>> GetUserFriends([FromQuery] BaseSpecParams baseParams, string userId)
		{
			return await _policePresentation.GetUserFriends(baseParams, userId);
		}

		[HttpPost("set-violation")]
		public async Task<ActionResult<bool>> SetViolation(ViolationDto violationDto)
		{
			return await _policePresentation.SetViolation(violationDto, HttpContext.User);
		}

		[HttpPost("get-user-violations/{userId}")]
		public async Task<TableData<ViolationViewDto>> GetUserViolations(TablePoliceParams tableParams, string userId)
		{
			return await _policePresentation.GetUserViolations(tableParams, userId);
		}

		[HttpDelete("amnesty-user/{amnestyId}")]
		public async Task<ActionResult<bool>> AmnestyUser(int amnestyId)
		{
			return await _policePresentation.AmnestyUser(amnestyId);
		}
	}
}
