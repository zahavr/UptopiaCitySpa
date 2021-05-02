using System;

namespace API.Dto
{
	public class RegisterDto
    {
		public string Email { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime BirthDate { get; set; }
		public string Password { get; set; }
	}
}
