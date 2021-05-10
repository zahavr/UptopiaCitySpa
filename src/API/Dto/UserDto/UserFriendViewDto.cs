using System;

namespace API.Dto
{
	public class UserFriendViewDto
    {
		public int Id { get; set; }
		public string FriendId { get; set; }
		public string FriendFirstName { get; set; }
		public string FriendLastName { get; set; }
		public DateTime FriendBirthDate { get; set; }
		public string FriendEmail { get; set; }
		public string ProfileImgUrl { get; set; }
	}
}
