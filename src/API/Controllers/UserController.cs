using API.Dto;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
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

		public UserController(
			  UserManager<User> userManager,
			  IMapper mapper,
			  IBlobService blobService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_blobService = blobService;
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
				
			if (await _blobService.UploadPhotoAsync(user.UserName, bytes, file.ContentType))
			{
				Uri fileUri = _blobService.GetPhoto(user.UserName);
				return fileUri.AbsoluteUri + $"?t={DateTime.Now}";
			}

			return BadRequest(new ApiResponse(401));
		}
	}
}
