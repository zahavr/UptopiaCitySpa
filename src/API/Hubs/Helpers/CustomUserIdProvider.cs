using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;

namespace API.Hubs.Helpers
{
	public class CustomUserIdProvider : IUserIdProvider
	{
		public string GetUserId(HubConnectionContext connection)
		{
			return connection.User?.FindFirst(ClaimTypes.Email)?.Value; ;
		}
	}
}
