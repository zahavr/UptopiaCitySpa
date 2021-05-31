using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace Telegram.Services
{
	public class BotService : IBotService
    {
		public BotService(IConfiguration configuration)
		{
			Client = new TelegramBotClient(configuration.GetValue<string>("TelegramToken"));
			ChatId = configuration.GetValue<string>("LoggerChatId");
		}

		public TelegramBotClient Client { get; }
		public string ChatId {get; }
	}
}
