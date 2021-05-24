using Core.Entities;

namespace Core.Specification
{
	public class UserWorkSpecification : BaseSpecification<BusinessWorker>
    {
		public UserWorkSpecification(string workerId)
			: base(uw => uw.WorkerId == workerId)
		{
			AddInclude(uw => uw.Business);
		}
    }
}
