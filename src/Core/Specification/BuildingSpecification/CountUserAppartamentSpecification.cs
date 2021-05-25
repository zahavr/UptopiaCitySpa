using Core.Entities;

namespace Core.Specification
{
	public class CountUserAppartamentSpecification : BaseSpecification<UserAppartament>
    {
		public CountUserAppartamentSpecification(string userId)
			: base(x => x.UserId == userId)
		{

		}
    }
}
