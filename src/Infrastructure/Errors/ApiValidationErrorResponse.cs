using System.Collections.Generic;

namespace Infrastructure.Erros
{
	public class ApiValidationErrorResponse : ApiResponse
    {
		public ApiValidationErrorResponse() : base(400)
		{

		}

		public IEnumerable<string> Errors { get; set; }
	}
}
