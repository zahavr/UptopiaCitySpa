using Microsoft.AspNetCore.Http;

namespace API.Dto
{
	public class FileUploadDto
    {
		public string Name { get; set; }
		public byte[] Length { get; set; }
		public string ContentType { get; set; }
		// public IFormFile File{ get; set; }
	}
}
