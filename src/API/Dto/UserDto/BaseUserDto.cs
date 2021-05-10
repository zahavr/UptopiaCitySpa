using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
	public class BaseUserDto
    {
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string PhoneNumber { get; set; }

		[Required]
		public DateTime BirthDate { get; set; }
	}
}
