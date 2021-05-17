using System.Collections.Generic;

namespace API.Dto.BuildingDto
{
	public class BuildingDto
    {
		public string Street { get; set; }
		public int CountFloor { get; set; }
		public int CountAppartaments { get; set; }
		public string PictureUrl { get; set; }
		public List<AppartamentDto> Appartaments { get; set; }
	}
}
