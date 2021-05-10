using API.Dto.BuildingDto;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification.BuildingSpecification;
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

		public Task<bool> AddNewBuildingAsync(BuildingDto buildingDto)
		{
			Building building = _mapper.Map<BuildingDto, Building>(buildingDto);

			return _buildingService.AddBuildingAsync(building);
		}

		public async Task<bool> BuyNewAppartamentAsync(User user, int appartamentId)
		{
			return await _buildingService.BuyAppartamentsAsync(user, appartamentId);
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
