using Core.Entities;

namespace Core.Specification
{
	public class CountUserPendingBusinessSpecification : BaseSpecification<Business>
    {
		public CountUserPendingBusinessSpecification(string userId)
			: base(b => b.OwnerId == userId && b.BusinessStatus == BusinessStatus.Pending)
		{
			
		}
    }
}
