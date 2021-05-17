using API.Hubs.Models;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs
{
	public class MessageHub : Hub
	{
		public async Task NewMessage(Message message)
		{
			message.TypeMessage = TypeMessage.Recieve;

			await Clients.User(message.Recipient).SendAsync("MessageReceived", message);
		}

		public async Task SendRequestForChat(RequestDialog requestDialog)
		{
			await Clients.User(requestDialog.RecipientEmail).SendAsync("NewChatRequest", requestDialog);
		}

		public async Task ConnectToDialog(string userEmail)
		{
			await Clients.User(userEmail).SendAsync("ConnectToDialog", true);
		}
	}
}
