using API.Dto.BuildingDto;
using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Presentation;
using Core.Entities.Identity;
using Core.Specification.BuildingSpecification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class BuildingController : BaseController
	{
		private readonly IBuildingPresentation _buildingPresentation;
		private readonly UserManager<User> _userManager;

		public BuildingController(
			IBuildingPresentation buildingPresentation,
			UserManager<User> userManager
			)
		{
			_buildingPresentation = buildingPresentation;
			_userManager = userManager;
		}

		[Authorize]
		[HttpGet("get-appartaments")]
		public async Task<ActionResult<Pagination<AppartamentViewDto>>> GetAppartaments([FromQuery] BuildingSpecParams buildingSpec)
		{
			return Ok(await _buildingPresentation.GetAppartaments(buildingSpec));
		}

		[Authorize]
		[HttpPost("add-building")]
		public async Task<ActionResult<BuildingDto>> AddNewBuilding(BuildingDto buildingDto)
		{
			if (buildingDto.CountApartments < buildingDto.Appartaments.Count)
				return BadRequest(new ApiResponse(400, "The number of apartments cannot be greater than the number of possible ones"));

			if (await _buildingPresentation.AddNewBuildingAsync(buildingDto))
				return Ok("You created new building");

			return BadRequest(new ApiResponse(400));
		}

		[Authorize]
		[HttpGet("buy-appartaments/{id}")]
		public async Task<ActionResult<bool>> BuyAppartament(int id)
		{
			User user = await _userManager
				.FindByEmailFromClaimsPrincipals(HttpContext.User);

			if (await _buildingPresentation.BuyNewAppartamentAsync(user, id))
				return Ok();

			return BadRequest(new ApiResponse(400, "Cannot buy this appartament"));
		}

		[Authorize]
		[HttpGet("get-appartament/{id}")]
		public async Task<ActionResult<AppartamentViewDto>> GetAppartament(int id)
		{
			return Ok(await _buildingPresentation.GetAppartament(id));
		}

		[Authorize]
		[HttpGet("get-random-appartaments")]
		public async Task<ActionResult<List<AppartamentViewDto>>> GetRandomAppartaments()
		{
			return Ok(await _buildingPresentation.GetRandomAppartament());
		}
	}
}
