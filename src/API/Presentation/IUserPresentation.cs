using API.Dto;
using API.Helpers;
using Core.Entities.Identity;
using Core.Specification.User;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IUserPresentation
    {
        Task<bool> PrependFriendRequest(User sender, UserFriendViewDto friendDto);
		Task<bool> AcceptFriend(int id);
		Task<bool> RejectFriend(int id);
		Task<Pagination<UserFriendViewDto>> GetFriendListRequest(UserFriendSpecParams specParams);
		Task<Pagination<UserFriendViewDto>> GetFriendList(UserFriendSpecParams specParams);
		Task<bool> DeleteFriendAsync(int id);
	}
}
