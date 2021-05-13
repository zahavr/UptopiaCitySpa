using Core.Entities;

namespace API.Dto.BusinessDto
{
	public class RespondVacancyDto
    {
		public string ApplicantId { get; set; }
		public int VacancyId { get; set; }
		public VacancyStatus VacancyStatus { get; set; }
	}
}
