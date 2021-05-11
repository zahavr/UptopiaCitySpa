using System;

namespace Core.Entities
{
	public class RejectedApplications : BaseEntity
    {
		public string OwnerId { get; set; }
		public string Description { get; set; }
		public DateTime ExpiredDate { get; set; }
	}
}
