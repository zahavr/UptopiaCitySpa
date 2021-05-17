namespace Core.Entities
{
	public class VacancyApplications : BaseEntity
    {
		public string ApplicantId { get; set; }
		public int VacancyId { get; set; }
		public VacancyStatus VacancyStatus { get; set; }
		public virtual Vacancy Vacancy { get; set; }
	}
}
