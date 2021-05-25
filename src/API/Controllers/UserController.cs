using API.Dto;
using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Presentation;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Core.Specification.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{

	public class UserController : BaseController
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IBlobService _blobService;
		private readonly IUserPresentation _userPresentation;

		public UserController(
			  UserManager<User> userManager,
			  IMapper mapper,
			  IBlobService blobService,
			  IUserPresentation userPresentation)
		{
			_userManager = userManager;
			_mapper = mapper;
			_blobService = blobService;
			_userPresentation = userPresentation;
		}

		[Authorize]
		[HttpGet("user-info")]
		public async Task<ActionResult<UserCabinetDto>> GetUserInfo()
		{
			User user = await _userManager
				.FindByEmailFromClaimsPrincipals(HttpContext.User);

			return _mapper.Map<User, UserCabinetDto>(user);
		}

		[Authorize]
		[HttpPost("upload-photo")]
		public async Task<ActionResult<string>> UploadUserAvatar(IFormFile file)
		{
			byte[] bytes = await file.GetBytesAsync();

			User user = await _userManager.FindByEmailFromClaimsPrincipals(HttpContext.User);

			if (await _blobService.UploadPhotoAsync(user.Email, bytes, file.ContentType))
			{
				Uri fileUri = _blobService.GetPhoto(user.Email);
				return fileUri.AbsoluteUri + $"?t={DateTime.Now}";
			}

			return BadRequest(new ApiResponse(400));
		}

		[Authorize]
		[HttpPatch("update-user")]
		public async Task<ActionResult<UserCabinetDto>> UpdateUserProfile(BaseUserDto userDto)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(HttpContext.User);

			_mapper.Map<BaseUserDto, User>(userDto, user);

			IdentityResult result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded) return BadRequest(new ApiResponse(400));

			return Ok(_mapper.Map<User, UserCabinetDto>(user));
		}

		[Authorize]
		[HttpGet("find-friend/{email}")]
		public async Task<ActionResult<UserFriendViewDto>> FindFriendByEmail(string email)
		{
			User friend = await _userManager.FindByEmailAsync(email);

			if (friend == null)
			{
				return BadRequest(new ApiResponse(400, "Sorry, user doesn`t exist"));
			}

			return _mapper.Map<User, UserFriendViewDto>(friend);
		}

		[Authorize]
		[HttpPost("create-friend-request")]
		public async Task<ActionResult> CreateFriendRequest(UserFriendViewDto friendDto)
		{
			if (friendDto == null)
			{
				return BadRequest(new ApiResponse(400, "Cannot send null"));
			}

			if (await _userPresentation.PrependFriendRequest(User, friendDto))
			{
				return Ok();
			}

			return BadRequest(new ApiResponse(400, "Fail request. Try later."));
		}

		[Authorize]
		[HttpGet("accept-friend-request/{id}")]
		public async Task<ActionResult> AcceptFrientRequest(int id)
		{
			if (await _userPresentation.AcceptFriend(id))
			{
				return Ok();
			}

			return BadRequest(new ApiResponse(400, "Cannot add this friend. Try later."));
		}

		[Authorize]
		[HttpGet("reject-friend-request/{id}")]
		public async Task<ActionResult<ApiResponse>> RejectFrientRequest(int id)
		{
			if (await _userPresentation.RejectFriend(id))
			{
				return Ok();
			}

			return BadRequest(new ApiResponse(400, "Cannot reject this request to friend. Try later."));
		}

		[Authorize]
		[HttpGet("list-request-friends")]
		public async Task<ActionResult<Pagination<UserFriendViewDto>>> ListFriendRequest([FromQuery] UserFriendSpecParams specParams)
		{
			return await _userPresentation.GetFriendListRequest(specParams);
		}

		[Authorize]
		[HttpGet("list-friends")]
		public async Task<ActionResult<Pagination<UserFriendViewDto>>> ListFriends([FromQuery] UserFriendSpecParams specParams)
		{
			return await _userPresentation.GetFriendList(specParams);
		}

		[Authorize]
		[HttpDelete("delete-friend/{id}")]
		public async Task<ActionResult<ApiResponse>> DeleteFriend(int id)
		{
			if (await _userPresentation.DeleteFriendAsync(id))
			{
				return Ok();
			}

			return BadRequest(new ApiResponse(400, "Cannot delete this friend"));
		}

		[Authorize]
		[HttpGet("get-violations")]
		public async Task<ActionResult<Pagination<ViolationViewDto>>> GetUserViolations([FromQuery] BaseSpecParams tableParams)
		{
			return await _userPresentation.GetUserViolations(tableParams, User);
		}

		[Authorize]
		[HttpDelete("pay-for-violation/{violationId}")]
		public async Task<ActionResult<ApiResponse>> PayForViolation(int violationId)
		{
			return await _userPresentation.PayForViolation(violationId, User);
		}
	}
}
