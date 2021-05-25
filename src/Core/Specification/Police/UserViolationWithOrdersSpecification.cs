using Core.Entities;

namespace Core.Specification
{
	public class UserViolationWithOrdersSpecification : BaseSpecification<Violation>
    {
		public UserViolationWithOrdersSpecification(TablePoliceParams tableParams, string suspectId)
			: base(v => v.CitizenId == suspectId)
		{
			ApplyTable(tableParams.First, tableParams.Rows);

			if (!string.IsNullOrEmpty(tableParams.SortField) && tableParams.SortOrder != 0)
			{
				if (tableParams.SortField.Equals("setDate") && tableParams.SortOrder == 1)
				{
					AddOrderBy(u => u.SetDate);
				}
				if (tableParams.SortField.Equals("setDate") && tableParams.SortOrder == -1)
				{
					AddOrderByDesc(u => u.SetDate);
				}
			}
		}
    }
}
