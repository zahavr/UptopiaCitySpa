using Core.Entities;

namespace Core.Specification
{
	public class UserAppartamentSpecification : BaseSpecification<UserAppartament>
    {
		public UserAppartamentSpecification(BaseSpecParams baseParams, string userId)
			: base(a => a.UserId == userId)
		{
			AddInclude(a => a.Appartament);
			ApplyPaging(baseParams.PageSize * (baseParams.PageIndex - 1), baseParams.PageSize);
		}
    }
}
