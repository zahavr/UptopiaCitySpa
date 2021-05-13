using Core.Entities;

namespace Core.Specification
{
	public class UserBusinessListSpecification : BaseSpecification<Business>
    {
		public UserBusinessListSpecification(BaseSpecParams specParams, string userId)
			: base(b => b.OwnerId == userId && b.BusinessStatus == BusinessStatus.Confirmed)
		{
			ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
		}
    }
}
