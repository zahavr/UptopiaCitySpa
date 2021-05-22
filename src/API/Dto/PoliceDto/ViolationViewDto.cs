using Core.Entities;
using System;

namespace API.Dto
{
	public class ViolationViewDto
    {
		public int Id { get; set; }
		public string Description { get; set; }
		public decimal Penalty { get; set; }
		public TypeViolation TypeViolation { get; set; }
		public DateTime DateExpired { get; set; }
		public DateTime SetDate { get; set; }
	}
}
