namespace Core
{
	public class ResultWithMessage
    {
		public ResultWithMessage(bool isSuccess, string message = null)
		{
			IsSuccess = isSuccess;
			Message = message;
		}
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}
