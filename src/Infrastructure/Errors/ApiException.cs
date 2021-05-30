namespace Infrastructure.Erros
{
	public class ApiException : ApiResponse
    {
		public ApiException(int statusCode, string message = null, string details = null, string path = null) : base(statusCode, message, path)
		{
			Details = details;
		}

		public string Details { get; set; }
	}
}
