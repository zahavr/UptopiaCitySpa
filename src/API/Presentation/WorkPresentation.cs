using API.Dto.WorkDto;
using Infrastructure.Erros;
using API.Extensions;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class WorkPresentation : IWorkPresentation
    {
		private readonly UserManager<User> _userManager;
		private readonly IWorkService _workService;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<BusinessWorker> _businessWorkerRepository;
		private readonly IGenericRepository<Shift> _shiftRepository;

		public WorkPresentation(
			UserManager<User> userManager,
			IWorkService workService,
			IMapper mapper,
			IGenericRepository<BusinessWorker> businessWorkerRepository,
			IGenericRepository<Shift> shiftRepository)
		{
			_userManager = userManager;
			_workService = workService;
			_mapper = mapper;
			_businessWorkerRepository = businessWorkerRepository;
			_shiftRepository = shiftRepository;
		}

		public async Task<bool> CheckUserOpenShift(ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);
			LastUserShiftSpecification spec = new LastUserShiftSpecification(user.Id);
			Shift shift = await _shiftRepository.GetEntityWithSpec(spec);

			return shift != null ? true : false;
		}

		public async Task<ActionResult<ApiResponse>> EndShift(ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);
			LastUserShiftSpecification spec = new LastUserShiftSpecification(user.Id);
			Shift shift = await _shiftRepository.GetEntityWithSpec(spec);

			if (shift == null)
			{
				return new BadRequestObjectResult(new ApiResponse(400, "You have`t shift"));
			}

			ResultWithMessage result = await _workService.EndShift(shift);

			if (!result.IsSuccess)
			{
				return new BadRequestObjectResult(new ApiResponse(400, result.Message));
			}

			return new OkObjectResult(new ApiResponse(200, result.Message));
		}

		public async Task<WorkViewDto> GetUserWork(ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			UserWorkSpecification spec = new UserWorkSpecification(user.Id);

			BusinessWorker businessWorker = await _businessWorkerRepository.GetEntityWithSpec(spec);

			return _mapper.Map<BusinessWorker, WorkViewDto>(businessWorker);
		}

		public async Task<bool> StartShift(ClaimsPrincipal claims)
		{
			User user = await _userManager.FindByEmailFromClaimsPrincipals(claims);

			return await _workService.StartShift(user);
		}

		
	}
}
