using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities.Identity
{
	public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}
