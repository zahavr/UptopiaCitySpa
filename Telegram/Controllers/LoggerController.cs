using Infrastructure.Erros;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Services;

namespace Telegram.Controllers
{
	[ApiController]
	[Route("api/{controller}")]
	public class LoggerController : ControllerBase
	{
		private readonly IUpdateService _updateService;

		public LoggerController(IUpdateService updateService)
		{
			_updateService = updateService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Update update)
		{
			await _updateService.SendAsync(update);

			return Ok();
		}

		[HttpPost("logs")]
		public async Task<IActionResult> SendLog(ApiException error)
		{
			await _updateService.SendLogAsync(error);

			return Ok();
		}
	}
}
