using Core.Entities;

namespace Core.Specification
{
	public class CountSpecialUserViolationSpecification : BaseSpecification<Violation>
    {
		public CountSpecialUserViolationSpecification(string userId)
			: base(v => v.CitizenId == userId)
		{

		}
    }
}
