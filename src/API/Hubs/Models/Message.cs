using System;

namespace API.Hubs.Models
{
	public class Message
    {
		public string Sender { get; set; }
		public string Recipient { get; set; }
		public string MessageText { get; set; }
		public TypeMessage TypeMessage { get; set; }
		public DateTime Date { get; set; }
	}
}
