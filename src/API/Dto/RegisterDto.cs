using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
	public class RegisterDto
    {
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6)]
		public string Login { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public DateTime BirthDate { get; set; }

		[Required]
		[RegularExpression("(?=^.{8,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$",
		ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alpanumeric and at least 6 characters")]
		public string Password { get; set; }
	}
}
