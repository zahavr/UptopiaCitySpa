using Core.Entities;

namespace Core.Specification
{
	public class ListUserRespondVacanciesSpecification : BaseSpecification<VacancyApplications>
    {
		public ListUserRespondVacanciesSpecification(TableParams tableParams,string userId)
			: base(va => va.ApplicantId == userId)
		{
			AddInclude(va => va.Vacancy);
			ApplyTable(tableParams.TableSkip, tableParams.TableTake);
		}
    }
}
