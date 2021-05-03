using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities.Identity
{
	public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
