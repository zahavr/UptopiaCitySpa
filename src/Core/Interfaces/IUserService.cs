using Core.Entities.Identity;
using Core.Entities.User;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IUserService
    {
        Task<bool> RecalculateMoney(User user, decimal money);
		Task<bool> CreateFriendRequest(Friend userFriend);
		Task<bool> AcceptFriend(int id);
		Task<bool> RejectFriend(int id);
		Task<bool> DeleteFriendAsync(Friend friend);
	}
}
