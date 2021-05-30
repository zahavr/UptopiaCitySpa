using Infrastructure.Erros;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Services
{
	public interface IUpdateService
    {
        Task SendAsync(Update update);
        Task SendLogAsync(ApiException err);
    }
}
