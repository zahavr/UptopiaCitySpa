using System;

namespace Core.Entities
{
	public class Appartament : BaseEntity
    {
		public int CountRooms { get; set; }
		public int Floor { get; set; }
		public TypeAppartament TypeAppartament { get; set; }
		public Guid ResidentId { get; set; }
		public decimal Cost { get; set; }

		public virtual Building Building { get; set; }
	}
}
