using API.Dto;
using Infrastructure.Erros;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
	public class AccountController : BaseController
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountController(
				UserManager<User> userManager,
				SignInManager<User> signInManager,
				ITokenService tokenService,
				IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if ((await CheckEmailExistAsync(registerDto.Email)).Value)
				return new BadRequestObjectResult(
					new ApiValidationErrorResponse
					{
						Errors = new[]
						{ "Email address in use" }
					});

			User user = _mapper.Map<RegisterDto, User>(registerDto);

			IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded) return BadRequest();

			IdentityResult resulForRole = await _userManager.AddToRoleAsync(user, "Citizen");

			if (!resulForRole.Succeeded) return BadRequest();

			return new UserDto
			{
				Money = user.Money,
				Login = user.UserName,
				Email = user.Email,
				Token = await _tokenService.CreateToken(user)
			};
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			User user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user == null) return BadRequest(new ApiResponse(401));

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (!result.Succeeded) return BadRequest(new ApiResponse(401));

			return new UserDto
			{
				Money = user.Money,
				Email = user.Email,
				Login = user.UserName,
				Token = await _tokenService.CreateToken(user)
			};
		}

		[Authorize]
		[HttpGet("current-user")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			User user = await _userManager
				.FindByEmailFromClaimsPrincipals(HttpContext.User);

			return new UserDto
			{
				Money = user.Money,
				Email = user.Email,
				Login = user.UserName,
				Token = await _tokenService.CreateToken(user)
			};
		}

		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
		{
			return await _userManager.FindByEmailAsync(email) != null;
		}
	}
}
