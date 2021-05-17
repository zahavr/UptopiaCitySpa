using API.Dto.BuildingDto;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification.BuildingSpecification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class BuildingPresntation : IBuildingPresentation
	{
		private readonly IGenericRepository<Appartament> _appartamentRepo;
		private readonly IBuildingService _buildingService;
		private readonly IMapper _mapper;

		public BuildingPresntation(
			IGenericRepository<Appartament> appartamentRepo,
			IBuildingService buildingService,
			IMapper mapper)
		{
			_appartamentRepo = appartamentRepo;
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
	}
}
