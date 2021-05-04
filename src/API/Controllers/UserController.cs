using API.Dto;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{

	public class UserController : BaseController
    {
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public UserController(
			  UserManager<User> userManager,
			  IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		[Authorize]
		[HttpGet("user-info")]
		public async Task<ActionResult<UserCabinetDto>> GetUserInfo()
		{
			User user = await _userManager
				.FindByEmailFromClaimsPrincipals(HttpContext.User);

			return _mapper.Map<User, UserCabinetDto>(user);
		}
	}
}
