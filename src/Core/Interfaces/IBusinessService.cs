using Core.Entities;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IBusinessService
	{
		Task<bool> CreateBusinessRequest(Business business);
		bool IsHasMoneyForOpenBusiness(User user, int maxCountOfWorker);
		Task<bool> AcceptBusinessRequest(Business business);
		Task<bool> RejectBusinessRequest(Business business, RejectedApplications rejectedApplication);
		Task<bool> CreateVacancyForBusiness(Vacancy vacancy);
		Task<bool> RespondVacancy(VacancyApplications vacancyApplications);
		Task<bool> AcceptWorker(VacancyApplications vacancy);
		Task<bool> DismissWorker(BusinessWorker businessWorker);
	}
}
