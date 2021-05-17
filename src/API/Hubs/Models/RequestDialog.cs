using System;

namespace API.Hubs.Models
{
	public class RequestDialog
    {
		public string SenderEmail { get; set; }
		public string RecipientEmail { get; set; }
		public DateTime Date {get; set; } 
	}
}
