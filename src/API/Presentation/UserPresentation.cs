using API.Dto;
using API.Helpers;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.User;
using Core.Interfaces;
using Core.Specification.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class UserPresentation : IUserPresentation
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IGenericRepository<Friend> _friendRepository;

		public UserPresentation(
			IMapper mapper,
			IUserService userService,
			IGenericRepository<Friend> friendRepository
			)
		{
			_mapper = mapper;
			_userService = userService;
			_friendRepository = friendRepository;
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

		public Task<bool> PrependFriendRequest(User sender, UserFriendViewDto friendDto)
		{
			Friend userFriend = _mapper.Map<User, Friend>(sender);

			userFriend = _mapper.Map<UserFriendViewDto, Friend>(friendDto, userFriend);
			userFriend.FriendStatus = FriendStatus.Pending;

			return _userService.CreateFriendRequest(userFriend);
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
	}
}
