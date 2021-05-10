using Core.Entities;

namespace API.Dto.BuildingDto
{
	public class AppartamentViewDto
    {
        public int Id { get; set; }
		public int CountRooms { get; set; }
		public int Floor { get; set; }
		public TypeAppartament TypeAppartament { get; set; }
		public decimal Cost { get; set; }
		public string Description { get; set; }
		public string Title { get; set; }
	}
}
