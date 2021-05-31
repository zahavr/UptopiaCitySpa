using Telegram.Bot;

namespace Telegram.Services
{
	public interface IBotService
	{
		TelegramBotClient Client { get; }
		string ChatId { get; }
	}
}
