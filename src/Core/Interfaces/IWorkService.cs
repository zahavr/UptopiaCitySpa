using Core.Entities;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IWorkService
	{
		Task<bool> StartShift(User user);
		Task<ResultWithMessage> EndShift(Shift shift);
	}
}
