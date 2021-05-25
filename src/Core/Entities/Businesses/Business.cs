using System;
using System.Collections.Generic;

namespace Core.Entities
{
	public class Business : BaseEntity
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public string OwnerId { get; set; }
		public string Address { get; set; }
		public int MaxCountOfWorker { get; set; }
		public BusinessStatus BusinessStatus { get; set; } = BusinessStatus.Confirmed;
		public DateTime DateConfirmation { get; set; }

		public virtual ICollection<BusinessWorker> BusinessWorkers { get; set; }
		public virtual ICollection<Vacancy> Vacancies{ get; set; }
	}
}
