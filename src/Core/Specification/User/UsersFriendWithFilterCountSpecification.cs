using Core.Entities.User;

namespace Core.Specification.User
{
	public class UsersFriendWithFilterCountSpecification : BaseSpecification<Friend>
    {
		public UsersFriendWithFilterCountSpecification(UserFriendSpecParams specParams)
			: base(f => f.UserEmail == specParams.UserEmail && f.FriendStatus == FriendStatus.Accepted &&
			string.IsNullOrEmpty(specParams.Search) || f.FriendEmail.ToLower().Contains(specParams.Search))
		{

		}
    }
}
