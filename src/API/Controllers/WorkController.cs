using API.Dto.WorkDto;
using Infrastructure.Erros;
using API.Presentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Authorize]
	public class WorkController : BaseController
	{
		private readonly IWorkPresentation _workPresentation;

		public WorkController(IWorkPresentation workPresentation)
		{
			_workPresentation = workPresentation;
		}

		[HttpGet("get-user-work")]
		public async Task<ActionResult<WorkViewDto>> GetUserWork()
		{
			return await _workPresentation.GetUserWork(HttpContext.User);
		}

		[HttpGet("check-open-shift")]
		public async Task<bool> CheckOpenShift()
		{
			return await _workPresentation.CheckUserOpenShift(User);
		}

		[HttpPost("start-shift")]
		public async Task<ActionResult<bool>> StartShift()
		{
			return await _workPresentation.StartShift(HttpContext.User);
		}

		[HttpPost("close-shift")]
		public async Task<ActionResult<ApiResponse>> EndShift()
		{
			return await _workPresentation.EndShift(HttpContext.User);
		}
	}
}
