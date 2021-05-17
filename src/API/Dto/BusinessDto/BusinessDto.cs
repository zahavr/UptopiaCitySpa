using System.ComponentModel.DataAnnotations;

namespace API.Dto.BusinessDto
{
	public class BusinessDto
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "Please fill name for your business")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please describe you business")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter your address")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Please enter count of workers")]
		public int MaxCountOfWorker { get; set; }
	}
}
