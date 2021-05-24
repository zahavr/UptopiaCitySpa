using Core.Entities;

namespace Core.Specification
{
	public class SpecialUserViolationSpecification : BaseSpecification<Violation>
    {
		public SpecialUserViolationSpecification(BaseSpecParams baseParams, string userId)
			: base(x => x.CitizenId == userId)
		{
			AddOrderBy(v => v.DateExpired);
			ApplyPaging(baseParams.PageSize * (baseParams.PageIndex - 1), baseParams.PageSize);
		}
    }
}
