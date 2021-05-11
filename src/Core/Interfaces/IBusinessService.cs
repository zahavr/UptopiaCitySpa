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
		Task<bool> CreateVacansyForBusiness(Vacancy vacancy);
	}
}
