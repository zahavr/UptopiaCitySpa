using System.Collections.Generic;

namespace Core.Entities
{
	public class Building : BaseEntity
    {
		public string Street { get; set; }
		public int CountFloor { get; set; }
		public int CountApartments { get; set; }
		
		public virtual ICollection<Appartament> Appartaments { get; set; }
	}
}
