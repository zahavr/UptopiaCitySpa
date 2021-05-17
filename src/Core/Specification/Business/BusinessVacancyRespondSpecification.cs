using Core.Entities;

namespace Core.Specification
{
	public class BusinessVacancyRespondSpecification : BaseSpecification<VacancyApplications>
    {
		public BusinessVacancyRespondSpecification(TableParams tableParams, int businessId)
			: base(va => va.Vacancy.BusinessId == businessId)
		{
			AddInclude(va => va.Vacancy);
			ApplyTable(tableParams.TableSkip, tableParams.TableTake);
		}
    }
}
