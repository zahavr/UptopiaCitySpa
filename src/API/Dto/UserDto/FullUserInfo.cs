using System.Collections.Generic;

namespace API.Dto
{
	public class FullUserInfo : BaseUserDto
    {
		public bool HasBusiness { get; set; }
		public bool HasAppartaments { get; set; }
		public IList<string> Roles { get; set; }
		public string PictureUrl { get; set; }
	}
}
