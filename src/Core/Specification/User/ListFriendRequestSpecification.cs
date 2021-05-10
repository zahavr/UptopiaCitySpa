using Core.Entities.User;

namespace Core.Specification.User
{
	public class ListFriendRequestSpecification : BaseSpecification<Friend>
    {
		public ListFriendRequestSpecification(UserFriendSpecParams specParams)
			: base (f => f.FriendEmail == specParams.UserEmail && f.FriendStatus == FriendStatus.Pending &&
			string.IsNullOrEmpty(specParams.Search) || f.UserEmail.ToLower().Contains(specParams.Search))
		{
			ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
		}
    }
}
