using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;

		public UserService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<bool> RecalculateMoney(User user, decimal money)
		{
			user.Money = user.Money - money;

			IdentityResult result = await _userManager.UpdateAsync(user);

			return result.Succeeded;
		}
	}
}
