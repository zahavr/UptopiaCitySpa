using API.Dto.BusinessDto;
using API.Helpers;
using Core.Specification;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IBusinessPresentation
    {
        Task<bool> CreateBusinessRequest(BusinessDto businessDto);
		Task<bool> AcceptBusinessRequest(int businessId);
		Task<bool> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto);
		Task<bool> CreateVacansy(BusinessVacancyDto businessVacancyDto);
		Task<bool> RespondVacancy(RespondVacancyDto respondVacancyDto, ClaimsPrincipal user);
		Task<TableData<BusinessDto>> GetBusinessRequests(TableParams tableParams);
		Task<Pagination<BusinessDto>> GetUserBusiness(BaseSpecParams specParams, ClaimsPrincipal claims);
		Task<TableData<BusinessDto>> GetPendingBuisness(TableParams tableParams, ClaimsPrincipal claims);
	}
}
