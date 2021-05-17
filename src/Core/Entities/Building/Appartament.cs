using System.Collections.Generic;

namespace Core.Entities
{
	public class Appartament : BaseEntity
	{
		public string Description { get; set; }
		public string Title { get; set; }
		public int CountRooms { get; set; }
		public int Floor { get; set; }
		public TypeAppartament TypeAppartament { get; set; }
		public decimal Cost { get; set; }
		public string PictureUrl { get; set; }
		public virtual Building Building { get; set; }
		public virtual ICollection<UserAppartament> UserAppartaments {get; set;}

	}
}
