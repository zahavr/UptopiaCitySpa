using Core.Entities;

namespace Core.Specification
{
	public class UserBusinessForPoliceSpecification : BaseSpecification<Business>
    {
		public UserBusinessForPoliceSpecification(string userId)
			: base(x => x.OwnerId == userId)
		{

		}
    }
}
