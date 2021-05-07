using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IUserService
    {
        Task<bool> RecalculateMoney(User user, decimal money);
    }
}
