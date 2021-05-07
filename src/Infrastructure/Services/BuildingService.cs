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

			return await _unitOfWork.Complete() >= 1;
		}

		public async Task<bool> BuyAppartamentsAsync(User user, int appartamentId)
		{
			IGenericRepository<Appartament> appartamentRepo = _unitOfWork.Repository<Appartament>();

			AppartamentWithUsersSpecification appartamentSpec = new AppartamentWithUsersSpecification(appartamentId);

			Appartament appartament = await appartamentRepo.GetEntityWithSpec(appartamentSpec);

			if (appartament.Cost > user.Money)
				return false;

			if (appartament.UserAppartaments.Count > appartament.CountRooms)
				return false;

			if (!await _userService.RecalculateMoney(user, appartament.Cost))
				return false;

			UserAppartament ownerAppartament = new UserAppartament
			{
				UserId = user.Id,
				AppartamentId = appartamentId
			};

			_unitOfWork.Repository<UserAppartament>()
				.Add(ownerAppartament);

			return await _unitOfWork.Complete() >= 1;
		}
	}
}
