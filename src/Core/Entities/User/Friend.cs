using System;

namespace Core.Entities.User
{
	public class Friend : BaseEntity
    {
		public string UserId { get; set; }
		public string UserFirstName { get; set; }
		public string UserLastName { get; set; }
		public DateTime BirthDateUser { get; set; }
		public string UserEmail { get; set; }
		public string FriendId { get; set; }
		public string FriendFirstName { get; set; }
		public string FriendLastName { get; set; }
		public DateTime FriendBirthDate { get; set; }
		public string FriendEmail { get; set; }
		public FriendStatus FriendStatus { get; set; }
	}
}
