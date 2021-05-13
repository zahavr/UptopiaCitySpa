using System.Collections.Generic;

namespace Core.Entities
{
	public class Vacancy : BaseEntity
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Salary { get; set; }
		public int BusinessId { get; set; }
		public virtual Business Business{ get; set; }
		public virtual ICollection<VacancyApplications> VacancyApplications { get; set; }
	}
}
