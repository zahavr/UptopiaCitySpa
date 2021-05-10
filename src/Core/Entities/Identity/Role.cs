using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
	public class Role : IdentityRole<string>
    {
		public string Description { get; set; }
	}
}
