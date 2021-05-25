using System;

namespace API.Dto.WorkDto
{
	public class WorkViewDto
    {
		public string PositionAtWork { get; set; }
		public string Salary { get; set; }
		public DateTime StartWork { get; set; }
		public double WorkExperience { get; set; }
		public string CompanyName { get; set; }
		public string CompanyAddress { get; set; }
	}
}
