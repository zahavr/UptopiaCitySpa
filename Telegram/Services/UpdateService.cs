using Infrastructure.Erros;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Services
{
	public class UpdateService : IUpdateService
	{
		private readonly IBotService _botService;

		public UpdateService(IBotService botService)
		{
			_botService = botService;
		}
		public async Task SendAsync(Update update)
		{
			// Will be some logic
		}

		public async Task SendLogAsync(ApiException error) { 
			await _botService.Client.SendTextMessageAsync(_botService.ChatId, $" Path: {error.Path}\n Status Code: {error.StatusCode}\n Message: {error.Message}\n Details: {error.Details}");
		}
	}
}
