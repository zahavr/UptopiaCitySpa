using Core.Entities;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IBuildingService
    {
        Task<bool> AddBuildingAsync(Building building);
		Task<bool> BuyAppartamentsAsync(User user, int appartamentId);
	}
}
