using System;

namespace Core.Entities
{
	public class BusinessWorker : BaseEntity
    {
		public string WorkerId { get; set; }
		public DateTime StartWork { get; set; }
		public string PositionAtWork { get; set; }
		public decimal Salary { get; set; }

		public int BusinessId { get; set; }
		public virtual Business Business { get; set; }
	}
}
