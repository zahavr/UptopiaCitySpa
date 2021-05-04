using System;

namespace API.Dto
{
	public class UserCabinetDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string PhoneNumber { get; set; }
		public string PictureUrl { get; set; }

		public int CalculateAge(DateTime birthDate)
		{
			int age = DateTime.Now.Year - BirthDate.Year;

			if (birthDate > DateTime.Now.AddYears(-age))
				age--;

			return age;
		}
	}
}
