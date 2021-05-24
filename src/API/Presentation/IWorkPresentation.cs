using API.Dto.WorkDto;
using API.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IWorkPresentation
    {
        Task<WorkViewDto> GetUserWork(ClaimsPrincipal claims);
		Task<bool> StartShift(ClaimsPrincipal claims);
		Task<ActionResult<ApiResponse>> EndShift(ClaimsPrincipal claims);
		Task<bool> CheckUserOpenShift(ClaimsPrincipal claims);
	}
}
