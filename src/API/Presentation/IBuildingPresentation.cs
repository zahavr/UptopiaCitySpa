using API.Dto.BuildingDto;
using API.Helpers;
using Core.Entities.Identity;
using Core.Specification.BuildingSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation
{
	public interface IBuildingPresentation
    {
        Task<bool> AddNewBuildingAsync(BuildingDto buildingDto);
        Task<bool> BuyNewAppartamentAsync(User user, int id);
        Task<Pagination<AppartamentViewDto>> GetAppartaments(BuildingSpecParams buildingSpecParams);
        Task<AppartamentViewDto> GetAppartament(int id);
        Task<IReadOnlyList<AppartamentViewDto>> GetRandomAppartament();
    }
}
