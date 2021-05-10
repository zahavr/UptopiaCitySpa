using Core.Entities.User;

namespace Core.Specification.User
{
	public class UserFriendRequestCountFilterSpecification : BaseSpecification<Friend>
    {
		public UserFriendRequestCountFilterSpecification(UserFriendSpecParams specParams)
			: base(f => f.FriendEmail == specParams.UserEmail && f.FriendStatus == FriendStatus.Pending &&
		   string.IsNullOrEmpty(specParams.Search) || f.UserEmail.ToLower().Contains(specParams.Search))
		{

		}
    }
}
