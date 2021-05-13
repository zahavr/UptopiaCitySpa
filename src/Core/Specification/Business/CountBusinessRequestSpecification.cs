using Core.Entities;

namespace Core.Specification
{
	public class CountBusinessRequestSpecification : BaseSpecification<Business>
    {
		public CountBusinessRequestSpecification()
			: base(b => b.BusinessStatus == BusinessStatus.Pending)
		{

		}
    }
}
