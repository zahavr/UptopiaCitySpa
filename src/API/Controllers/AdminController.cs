using API.Dto;
using API.Errors;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : BaseController
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public AdminController(
			RoleManager<Role> roleManager,
			UserManager<User> userManager,
			IMapper mapper)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
		}

		[HttpPost("create-userrole")]
		public async Task<ActionResult<bool>> CreateNewRole(RoleDto roleDto)
		{
			Role role = _mapper.Map<RoleDto, Role>(roleDto);

			if (await _roleManager.RoleExistsAsync(role.Name)) return BadRequest(new ApiResponse(400, "Role already exist"));

			role.Id = Guid.NewGuid().ToString();

			IdentityResult result = await _roleManager.CreateAsync(role);

			return result.Succeeded ? Ok() : BadRequest(new ApiResponse(400, "Cannot create this role"));
		}

		[HttpPost("assign-role")]
		public async Task<ActionResult<bool>> AssignRoleForUser(AssignRoleDto assignRoleDto)
		{
			User user = await _userManager.FindByIdAsync(assignRoleDto.UserId);

			IdentityResult result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);

			return result.Succeeded ? Ok() : BadRequest("Cannot add this role");
		}
	}
}
