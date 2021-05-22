using Core.Entities;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IPoliceService
	{
		Task<bool> UserHasBusiness(string id);
		Task<bool> UserHasAppartaments(string id);
		Task<bool> SetViolation(Violation violation, User policeman);
		Task<bool> AmnestyUser(Violation amnestyId);
	}
}
