using Core.Entities;

namespace Core.Specification
{
    public class CountUserBusinessSpecififcation : BaseSpecification<Business>
    {
		public CountUserBusinessSpecififcation(string userId)
			: base(b => b.BusinessStatus == BusinessStatus.Confirmed && b.OwnerId == userId)
		{

		}
    }
}
