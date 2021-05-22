using API.Dto;
using Core.Specification;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IPolicePresentation
    {
        Task<TableData<UserCabinetDto>> GetUsers(TablePoliceParams tableParams);
		Task<FullUserInfo> GetUser(string id);
		Task<IReadOnlyList<AppartamentForPoliceDto>> GetUserAppartaments(string userId);
		Task<IReadOnlyList<BusinessDto>> GetUserBusiness(string userId);
		Task<IReadOnlyList<UserFriendViewDto>> GetUserFriends(BaseSpecParams baseParams, string userId);
		Task<bool> SetViolation(ViolationDto violationDto, ClaimsPrincipal user);
		Task<TableData<ViolationViewDto>> GetUserViolations(TablePoliceParams tableParams, string userId);
		Task<bool> AmnestyUser(int amnestyId);
	}
}
