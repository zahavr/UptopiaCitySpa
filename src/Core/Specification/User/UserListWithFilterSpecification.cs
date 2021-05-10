using Core.Entities.User;

namespace Core.Specification.User
{
	public class UserListWithFilterSpecification : BaseSpecification<Friend>
    {
		public UserListWithFilterSpecification(UserFriendSpecParams specParams)
			: base(f => f.UserEmail == specParams.UserEmail && f.FriendStatus == FriendStatus.Accepted &&
			string.IsNullOrEmpty(specParams.Search) || f.FriendEmail.ToLower().Contains(specParams.Search))
		{
			ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
		}
    }
}
