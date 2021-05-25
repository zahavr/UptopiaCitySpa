using Core.Entities;

namespace Core.Specification.BuildingSpecification
{
	public class UserAppartamentsForPoliceSpecification : BaseSpecification<UserAppartament>
    {
		public UserAppartamentsForPoliceSpecification(string userId)
			: base (up => up.UserId == userId)
		{
			AddInclude(up => up.Appartament);
		}
    }
}
