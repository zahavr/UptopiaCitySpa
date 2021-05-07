using Core.Entities;

namespace Core.Specification.BuildingSpecification
{
	public class AppartamentSpecification : BaseSpecification<Appartament>
    {
		public AppartamentSpecification(BuildingSpecParams buildingSpecParams) 
			: base(ap => ap.UserAppartaments.Count == 0)
		{
			AddInclude(x => x.UserAppartaments);
			ApplyPaging(buildingSpecParams.PageSize * (buildingSpecParams.PageIndex - 1), buildingSpecParams.PageSize);
		}
    }
}
