using Core.Entities;

namespace API.Dto
{
	public class AppartamentForPoliceDto
    {
		public string Title { get; set; }
		public decimal Cost { get; set; }
		public int CountRooms { get; set; }
		public TypeAppartament TypeAppartament { get; set; }
	}
}
