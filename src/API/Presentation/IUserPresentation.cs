using API.Dto;
using API.Errors;
using API.Helpers;
using Core.Specification;
using Core.Specification.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IUserPresentation
    {
        Task<bool> PrependFriendRequest(ClaimsPrincipal sender, UserFriendViewDto friendDto);
		Task<bool> AcceptFriend(int id);
		Task<bool> RejectFriend(int id);
		Task<Pagination<UserFriendViewDto>> GetFriendListRequest(UserFriendSpecParams specParams);
		Task<Pagination<UserFriendViewDto>> GetFriendList(UserFriendSpecParams specParams);
		Task<bool> DeleteFriendAsync(int id);
		Task<Pagination<ViolationViewDto>> GetUserViolations(BaseSpecParams tableParams, ClaimsPrincipal claims);
		Task<ApiResponse> PayForViolation(int violationId, ClaimsPrincipal user);
	}
}
