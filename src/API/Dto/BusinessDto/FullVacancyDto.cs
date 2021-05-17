namespace API.Dto.BusinessDto
{
    public class FullVacancyDto
    {
		public int VacancyId { get; set; }
		public string VacancyTitle { get; set; }
		public string VacancyDescription { get; set; }
		public decimal Salary { get; set; }
		public string BusinessTitle { get; set; }
		public string BusinessDescription { get; set; }
		public string Address { get; set; }
	}
}
