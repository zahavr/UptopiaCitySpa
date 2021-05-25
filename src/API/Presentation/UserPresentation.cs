using API.Dto;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.User;
using Core.Interfaces;
using Core.Specification;
using Core.Specification.User;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class UserPresentation : IUserPresentation
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly UserManager<User> _userManager;
		private readonly IGenericRepository<Friend> _friendRepository;
		private readonly IGenericRepository<Violation> _violationRepository;

		public UserPresentation(
			IMapper mapper,
			IUserService userService,
			UserManager<User> userManager,
			IGenericRepository<Friend> friendRepository,
			IGenericRepository<Violation> violationRepository
			)
		{
			_mapper = mapper;
			_userService = userService;
			_userManager = userManager;
			_friendRepository = friendRepository;
			_violationRepository = violationRepository;
		}

		public async Task<bool> AcceptFriend(int id)
		{
			return await _userService.AcceptFriend(id);
		}

		public async Task<bool> RejectFriend(int id)
		{
			return await _userService.RejectFriend(id);
		}

		public async Task<Pagination<UserFriendViewDto>> GetFriendListRequest(UserFriendSpecParams specParams)
		{
			ListFriendRequestSpecification friendSpec = new ListFriendRequestSpecification(specParams);
			UserFriendRequestCountFilterSpecification countSpec = new UserFriendRequestCountFilterSpecification(specParams);

			IReadOnlyList<Friend> data = await _friendRepository.ListAsync(friendSpec);

			int count = await _friendRepository.CountAsync(countSpec);
			List<UserFriendViewDto> resultData = _mapper.Map<IReadOnlyList<Friend>, List<UserFriendViewDto>>(data);

			return new Pagination<UserFriendViewDto>(specParams.PageIndex, specParams.PageSize, count, resultData);
		}

		public async Task<bool> PrependFriendRequest(ClaimsPrincipal sender, UserFriendViewDto friendDto)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(sender);

			Friend userFriend = _mapper.Map<User, Friend>(user);

			userFriend = _mapper.Map<UserFriendViewDto, Friend>(friendDto, userFriend);
			userFriend.FriendStatus = FriendStatus.Pending;

			return await _userService.CreateFriendRequest(userFriend);
		}

		public async Task<Pagination<UserFriendViewDto>> GetFriendList(UserFriendSpecParams specParams)
		{
			UserListWithFilterSpecification spec = new UserListWithFilterSpecification(specParams);
			UsersFriendWithFilterCountSpecification specCount = new UsersFriendWithFilterCountSpecification(specParams);

			IReadOnlyList<Friend> data = await _friendRepository.ListAsync(spec);
			
			int count = await _friendRepository.CountAsync(specCount);
			List<UserFriendViewDto> resultData = _mapper.Map<IReadOnlyList<Friend>, List<UserFriendViewDto>>(data);

			return new Pagination<UserFriendViewDto>(specParams.PageIndex, specParams.PageSize, count, resultData);
		}

		public async Task<bool> DeleteFriendAsync(int id)
		{
			Friend friend = await _friendRepository.GetByIdAsync(id);

			return await _userService.DeleteFriendAsync(friend);
		}

		public async Task<Pagination<ViolationViewDto>> GetUserViolations(BaseSpecParams tableParams, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			CountSpecialUserViolationSpecification countSpec = new CountSpecialUserViolationSpecification(user.Id);
			SpecialUserViolationSpecification spec = new SpecialUserViolationSpecification(tableParams, user.Id);

			int totalCount = await _violationRepository.CountAsync(countSpec);
			IReadOnlyList<Violation> data = await _violationRepository.ListAsync(spec);

			List<ViolationViewDto> result = _mapper.Map<IReadOnlyCollection<Violation>, List<ViolationViewDto>>(data);

			return new Pagination<ViolationViewDto>(tableParams.PageIndex, tableParams.PageSize, totalCount, result);
		}

		public async Task<ApiResponse> PayForViolation(int violationId, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);
			Violation violation = await _violationRepository.GetByIdAsync(violationId);

			ResultWithMessage result = await _userService.PayViolation(user, violation);

			if (!result.IsSuccess)
			{
				return new ApiResponse(400, result.Message);
			}

			return new ApiResponse(200, result.Message);
		}
	}
}
