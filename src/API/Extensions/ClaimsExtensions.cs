using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
	public static class ClaimsExtensions
    {
        public static string CheckEmailExistsAsync(this ClaimsPrincipal user)
            => user?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        
    }
}
