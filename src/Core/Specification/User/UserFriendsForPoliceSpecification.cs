using Core.Entities.User;

namespace Core.Specification
{
	public class UserFriendsForPoliceSpecification : BaseSpecification<Friend>
    {
		public UserFriendsForPoliceSpecification(BaseSpecParams baseParams, string userId)
			: base(f => f.UserId == userId)
		{
			ApplyPaging(baseParams.PageSize * (baseParams.PageIndex - 1), baseParams.PageSize);
		}
    }
}
