using Core.Entities;

namespace Core.Specification
{
	public class ListBusinessWorkersForBusinessSpecification : BaseSpecification<BusinessWorker>
    {
		public ListBusinessWorkersForBusinessSpecification(TableParams tableParams, int businessId)
			: base(bw => bw.BusinessId == businessId)
		{
			ApplyTable(tableParams.TableSkip, tableParams.TableTake);
		}
    }
}
