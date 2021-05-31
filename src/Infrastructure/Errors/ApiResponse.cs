using Microsoft.Extensions.Logging;

namespace Infrastructure.Erros
{
	public class ApiResponse
	{
		public ApiResponse(int statusCode, string message = null, string path = null)
		{
			StatusCode = statusCode;
			Path = path;
			Message = message ?? GetMessageForStatusCode(statusCode);
		}
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public string Path {get; set;}
		private string GetMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				200 => "All good",
				400 => "A bad request, you have made",
				401 => "Authorized, you are not",
				404 => "Resource found, it was not",
				500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
				_ => null
			};
		}
	}
}
