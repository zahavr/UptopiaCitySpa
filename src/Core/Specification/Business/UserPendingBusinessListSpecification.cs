using Core.Entities;

namespace Core.Specification
{
	public class UserPendingBusinessListSpecification : BaseSpecification<Business>
    {
		public UserPendingBusinessListSpecification(TableParams tableParams, string userId)
			: base(b => b.OwnerId == userId && b.BusinessStatus == BusinessStatus.Pending)
		{
			ApplyTable(tableParams.TableSkip, tableParams.TableTake);
		}
	}
}
