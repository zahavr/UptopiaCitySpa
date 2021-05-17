using Core.Entities;

namespace Core.Specification
{
	public class CountBusinessWorkersForBusinessSpecification : BaseSpecification<BusinessWorker>
    {
		public CountBusinessWorkersForBusinessSpecification(int businessId)
			: base(bw => bw.BusinessId == businessId)
		{

		}
    }
}
