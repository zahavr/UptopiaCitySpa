using System;

namespace Core.Entities
{
	public class Violation : BaseEntity
	{
		public string PolicemanId { get; set; }
		public string CitizenId { get; set; }
		public string Description { get; set; }
		public decimal Penalty { get; set; }
		public TypeViolation TypeViolation { get; set; }
		public DateTime DateExpired { get; set; }
		public DateTime SetDate { get; set; }
	}
}
