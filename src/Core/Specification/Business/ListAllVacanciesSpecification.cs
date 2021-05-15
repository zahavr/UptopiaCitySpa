using Core.Entities;

namespace Core.Specification
{
	public class ListAllVacanciesSpecification : BaseSpecification<Vacancy>
    {
		public ListAllVacanciesSpecification(BaseSpecParams tableParams)
		{
			AddInclude(v => v.Business);
			ApplyPaging(tableParams.PageSize * (tableParams.PageIndex - 1), tableParams.PageSize);
		}
    }
}
