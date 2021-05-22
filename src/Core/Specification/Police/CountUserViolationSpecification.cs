using Core.Entities;

namespace Core.Specification
{
	public class CountUserViolationSpecification : BaseSpecification<Violation>
    {
		public CountUserViolationSpecification(string suspectId)
			: base(x => x.CitizenId == suspectId)
		{

		}
    }
}
