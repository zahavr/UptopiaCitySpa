using Core.Entities;

namespace Core.Specification.BuildingSpecification
{
	public class CountFreeAppartamentSpecification : BaseSpecification<Appartament>
    {
		public CountFreeAppartamentSpecification()
			: base(ap => ap.UserAppartaments.Count == 0)
		{
			AddInclude(ap => ap.UserAppartaments);
		}
    }
}
