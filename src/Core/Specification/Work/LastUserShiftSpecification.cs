using Core.Entities;

namespace Core.Specification
{
	public class LastUserShiftSpecification : BaseSpecification<Shift>
    {
		public LastUserShiftSpecification(string userId)
			: base(s => s.UserId == userId && s.EndShift == null)
		{
		}
    }
}
