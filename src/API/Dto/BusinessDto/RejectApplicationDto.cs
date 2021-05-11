using System;

namespace API.Dto.BusinessDto
{
	public class RejectApplicationDto
    {
		public int BusinessId { get; set; }
		public string OwnerId { get; set; }
		public string Description { get; set; }
		public DateTime ExpiredDate { get; set; }
	}
}
