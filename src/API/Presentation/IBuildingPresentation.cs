using API.Dto.BuildingDto;
using API.Errors;
using API.Helpers;
using Core.Entities.Identity;
using Core.Specification;
using Core.Specification.BuildingSpecification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IBuildingPresentation
    {
        Task<ActionResult<ApiResponse>> AddNewBuildingAsync(BuildingDto buildingDto);
        Task<ActionResult<ApiResponse>> BuyNewAppartamentAsync(User user, int id);
        Task<Pagination<AppartamentViewDto>> GetAppartaments(BuildingSpecParams buildingSpecParams);
        Task<AppartamentViewDto> GetAppartament(int id);
        Task<IReadOnlyList<AppartamentViewDto>> GetRandomAppartament();
		Task<Pagination<AppartamentViewDto>> GetUserAppartament(BaseSpecParams baseSpec, ClaimsPrincipal claims);
		Task<bool> SellAppartament(int id, ClaimsPrincipal claims);
	}
}
