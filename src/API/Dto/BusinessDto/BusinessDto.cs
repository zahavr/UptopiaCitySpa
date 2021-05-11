using System.ComponentModel.DataAnnotations;

namespace API.Dto.BusinessDto
{
	public class BusinessDto
    {
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public int MaxCountOfWorker { get; set; }
	}
}
