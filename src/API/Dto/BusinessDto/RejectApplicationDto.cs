using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.BusinessDto
{
	public class RejectApplicationDto
    {
		[Required]
		public int BusinessId { get; set; }
		public string OwnerId { get; set; }
		[Required]
		[MaxLength(400)]
		public string Description { get; set; }
		[Required]
		public DateTime ExpiredDate { get; set; }
	}
}
