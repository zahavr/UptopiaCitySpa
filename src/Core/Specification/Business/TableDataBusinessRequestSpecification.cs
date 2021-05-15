using Core.Entities;

namespace Core.Specification
{
	public class TableDataBusinessRequestSpecification : BaseSpecification<Business>
    {
		public TableDataBusinessRequestSpecification(TableParams tableParams) :
			base(b => b.BusinessStatus == BusinessStatus.Pending)
		{
			ApplyTable(tableParams.TableSkip, tableParams.TableTake);
		}
    }
}
