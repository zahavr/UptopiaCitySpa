using Core.Entities;

namespace Core.Specification.BuildingSpecification
{
	public class AppartamentWithUsersSpecification : BaseSpecification<Appartament>
    {
		public AppartamentWithUsersSpecification(int id) :
			base(x => x.Id == id)
		{
			AddInclude(x => x.UserAppartaments);
		}
    }
}
