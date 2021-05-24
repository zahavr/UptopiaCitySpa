using System;

namespace Core.Entities
{
	public class Shift : BaseEntity
    {
		public string UserId { get; set; }
		public DateTime StartShift { get; set; }
		public DateTime? EndShift { get; set; }
		public decimal? EarnedMoney { get; set; }
	}
}
