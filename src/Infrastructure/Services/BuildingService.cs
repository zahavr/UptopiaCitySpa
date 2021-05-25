using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification.BuildingSpecification;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class BuildingService : IBuildingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserService _userService;

		public BuildingService(
			IUnitOfWork unitOfWork,
			IUserService userService)
		{
			_unitOfWork = unitOfWork;
			_userService = userService;
		}

		public async Task<bool> AddBuildingAsync(Building building)
		{
			_unitOfWork.Repository<Building>()
			   .Add(building);

			_unitOfWork.Repository<Appartament>()
				.AddRange(building.Appartaments);

			return await _unitOfWork.Complete();
		}

		public async Task<ResultWithMessage> BuyAppartamentsAsync(User user, int appartamentId)
		{
			ResultWithMessage result = new ResultWithMessage(false);

			IGenericRepository<Appartament> appartamentRepo = _unitOfWork.Repository<Appartament>();

			AppartamentWithUsersSpecification appartamentSpec = new AppartamentWithUsersSpecification(appartamentId);

			Appartament appartament = await appartamentRepo.GetEntityWithSpec(appartamentSpec);

			if (appartament.Cost > user.Money)
			{
				result.Message = "You can`t buy this appartament. You need more money";
				return result;
			}

			if (!await _userService.RecalculateMoney(user, appartament.Cost))
			{
				result.Message = "Cannot recalculate money";
				return result;
			}

			UserAppartament ownerAppartament = new UserAppartament
			{
				UserId = user.Id,
				AppartamentId = appartamentId
			};

			_unitOfWork.Repository<UserAppartament>()
				.Add(ownerAppartament);

			if (!await _unitOfWork.Complete())
			{
				result.Message = "Cannot complete operation please try later";
				return result;
			}

			result.IsSuccess = true;
			result.Message = "Congratulations! You bought new appartament";

			return result;
		}

		public async Task<bool> SellAppartament(User user, UserAppartament appartament)
		{
			if (!await _userService.RecalculateMoney(user, appartament.Appartament.Cost))
			{
				return false;
			};

			_unitOfWork.Repository<UserAppartament>().Delete(appartament);

			return await _unitOfWork.Complete();
		}
	}
}
