using Core.Entities.User;

namespace Core.Specification.User
{
	public class UserForDeleteSpecification : BaseSpecification<Friend>
    {
		public UserForDeleteSpecification(string userId, string friendId)
			: base(f => f.UserId == friendId && f.FriendId == userId)
		{

		}
    }
}
