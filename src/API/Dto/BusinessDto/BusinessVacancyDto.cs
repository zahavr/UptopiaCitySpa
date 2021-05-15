using System.ComponentModel.DataAnnotations;

namespace API.Dto.BusinessDto
{
	public class BusinessVacancyDto
    {
		[Required]
		[MaxLength(50)]
		public string Title { get; set; }
		[Required]
		[MaxLength(1000)]
		public string Description { get; set; }
		[Required]
		public decimal Salary { get; set; }
		public int BusinessId { get; set; }
	}
}
