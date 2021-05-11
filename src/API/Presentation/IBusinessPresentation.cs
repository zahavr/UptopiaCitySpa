using API.Dto;
using API.Dto.BusinessDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IBusinessPresentation
    {
        Task<bool> CreateBusinessRequest(BusinessDto businessDto);
		Task<bool> AcceptBusinessRequest(AcceptBusinessDto acceptBusinessDto);
		Task<bool> RejectBusinessRequest(RejectApplicationDto rejectApplicationDto);
		Task<bool> CreateVacansy(BusinessVacancyDto businessVacancyDto);
	}
}
