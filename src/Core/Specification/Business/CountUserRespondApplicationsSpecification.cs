using Core.Entities;

namespace Core.Specification
{
	public class CountUserRespondApplicationsSpecification : BaseSpecification<VacancyApplications>
    {
		public CountUserRespondApplicationsSpecification(string userId)
			: base(va => va.ApplicantId == userId)
		{

		}
    }
}
