using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BuggyController : BaseController
    {
		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			return NotFound(new ApiResponse(404));			
		}

		[HttpGet("testauth")]
		[Authorize]
		public ActionResult<string> GetSecretText()
		{
			return "secret stuff";
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			User user = null;

			string createError = user.ToString();

			return Ok();
		}

		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetNotFoundRequest(int id)
		{
			return BadRequest(new ApiResponse(400));
		}
	}
}
