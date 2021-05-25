using API.Dto.BuildingDto;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Core.Specification.BuildingSpecification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class BuildingPresntation : IBuildingPresentation
	{
		private readonly UserManager<User> _userManager;
		private readonly IGenericRepository<Appartament> _appartamentRepo;
		private readonly IGenericRepository<UserAppartament> _userAppartamentRepo;
		private readonly IBuildingService _buildingService;
		private readonly IMapper _mapper;

		public BuildingPresntation(
			UserManager<User> userManager,
			IGenericRepository<Appartament> appartamentRepo,
			IGenericRepository<UserAppartament> userAppartamentRepo,
			IBuildingService buildingService,
			IMapper mapper)
		{
			_userManager = userManager;
			_appartamentRepo = appartamentRepo;
			_userAppartamentRepo = userAppartamentRepo;
			_buildingService = buildingService;
			_mapper = mapper;
		}

		public async Task<ActionResult<ApiResponse>> AddNewBuildingAsync(BuildingDto buildingDto)
		{
			Building building = _mapper.Map<BuildingDto, Building>(buildingDto);

			if (await _buildingService.AddBuildingAsync(building))
			{
				return new OkObjectResult(new ApiResponse(200, "You created new building"));
			}

			return new BadRequestObjectResult(new ApiResponse(400));
		}

		public async Task<ActionResult<ApiResponse>> BuyNewAppartamentAsync(User user, int appartamentId)
		{
			ResultWithMessage result = await _buildingService.BuyAppartamentsAsync(user, appartamentId);

			if (result.IsSuccess)
			{
				return new OkObjectResult(new ApiResponse(200, result.Message));
			}

			return new BadRequestObjectResult(new ApiResponse(400, result.Message));
		}

		public async Task<Pagination<AppartamentViewDto>> GetAppartaments(BuildingSpecParams buildingSpecParams)
		{
			AppartamentSpecification spec = new AppartamentSpecification(buildingSpecParams);
			CountFreeAppartamentSpecification countSpec = new CountFreeAppartamentSpecification();

			IReadOnlyList<Appartament> appartaments = await _appartamentRepo.ListAsync(spec);
			IReadOnlyList<AppartamentViewDto> data =
				_mapper.Map<IReadOnlyList<Appartament>, IReadOnlyList<AppartamentViewDto>>(appartaments);

			int totalCount = await _appartamentRepo.CountAsync(countSpec);

			return new Pagination<AppartamentViewDto>(buildingSpecParams.PageIndex, buildingSpecParams.PageSize, totalCount, data);
		}

		public async Task<AppartamentViewDto> GetAppartament(int id)
		{
			Appartament appartament = await _appartamentRepo.GetByIdAsync(id);

			return _mapper.Map<Appartament, AppartamentViewDto>(appartament);
		}

		public async Task<IReadOnlyList<AppartamentViewDto>> GetRandomAppartament()
		{
			CountFreeAppartamentSpecification countSpec = new CountFreeAppartamentSpecification();
			int totalCount = await _appartamentRepo.CountAsync(countSpec);

			RandomAppartamentsSpecification appartamentsSpecification = new RandomAppartamentsSpecification(totalCount);
			IReadOnlyList<Appartament> appartaments = await _appartamentRepo.ListAsync(appartamentsSpecification);

			return _mapper.Map<IReadOnlyList<Appartament>, IReadOnlyList<AppartamentViewDto>>(appartaments);
		}

		public async Task<Pagination<AppartamentViewDto>> GetUserAppartament(BaseSpecParams baseSpec, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			CountUserAppartamentSpecification countSpec = new CountUserAppartamentSpecification(user.Id);
			UserAppartamentSpecification spec = new UserAppartamentSpecification(baseSpec, user.Id);

			int totalCount = await _userAppartamentRepo.CountAsync(countSpec);

			IReadOnlyList<UserAppartament> appartaments = await _userAppartamentRepo.ListAsync(spec);
			IReadOnlyList<AppartamentViewDto> result = _mapper.Map<IReadOnlyList<UserAppartament>, IReadOnlyList<AppartamentViewDto>>(appartaments);

			return new Pagination<AppartamentViewDto>(baseSpec.PageIndex, baseSpec.PageSize, totalCount, result);
		}

		public async Task<bool> SellAppartament(int id, ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			UserAppartament appartament = await _userAppartamentRepo.GetAll()
																	.Include(x => x.Appartament)
																	.FirstOrDefaultAsync(x => x.AppartamentId == id);

			return await _buildingService.SellAppartament(user, appartament);
		}
	}
}
