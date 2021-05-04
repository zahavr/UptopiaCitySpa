using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<User> FindByEmailFromClaimsPrincipals(this UserManager<User> userManager, ClaimsPrincipal user)
		{
			string email = user.Claims?
				.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

			return await userManager.Users?.SingleOrDefaultAsync(user => user.Email == email);
		}
	}
}
