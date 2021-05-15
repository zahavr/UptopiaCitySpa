using Core.Entities;

namespace Core.Specification
{
	public class CountBusinessVacancyRespondSpecification : BaseSpecification<VacancyApplications>
    {
		public CountBusinessVacancyRespondSpecification(int businessId)
			: base(va => va.Vacancy.BusinessId == businessId)
		{
			AddInclude(va => va.Vacancy);
		}
    }
}
