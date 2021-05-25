using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.User;
using Core.Interfaces;
using Core.Specification.User;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(
			UserManager<User> userManager,
			IUnitOfWork unitOfWork
			)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> AcceptFriend(int id)
		{
			Friend friendRequest = await _unitOfWork.Repository<Friend>().GetByIdAsync(id);

			friendRequest.FriendStatus = FriendStatus.Accepted;

			Friend friendForFriend = CreateFriendForFriend(friendRequest);

			_unitOfWork.Repository<Friend>().Update(friendRequest);
			_unitOfWork.Repository<Friend>().Add(friendForFriend);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> CreateFriendRequest(Friend userFriend)
		{
			_unitOfWork.Repository<Friend>().Add(userFriend);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> DeleteFriendAsync(Friend friend)
		{
			IGenericRepository<Friend> repository = _unitOfWork.Repository<Friend>();

			UserForDeleteSpecification spec = new UserForDeleteSpecification(friend.UserId, friend.FriendId);

			Friend deleteFriend = await repository.GetEntityWithSpec(spec);

			repository.Delete(deleteFriend);
			repository.Delete(friend);

			return await _unitOfWork.Complete();
		}

		public async Task<ResultWithMessage> PayViolation(User user, Violation violation)
		{
			ResultWithMessage result = new ResultWithMessage(false);

			if (user.Money < violation.Penalty)
			{
				result.Message = "You`ve less money than you penalty";
				return result;
			}

			user.Money -= violation.Penalty;

			IdentityResult isUpdateUser = await _userManager.UpdateAsync(user);

			if (!isUpdateUser.Succeeded)
			{
				result.Message = "Cannot update budget";
				return result;
			}

			_unitOfWork.Repository<Violation>().Delete(violation);

			if (!await _unitOfWork.Complete())
			{
				result.Message = "Cannot delete violation";
				return result;
			}
			result.IsSuccess = true;
			result.Message = "Violation was deleted";
			return result;
		}

		public async Task<bool> RecalculateMoney(User user, decimal money)
		{
			user.Money = user.Money - money;

			IdentityResult result = await _userManager.UpdateAsync(user);

			return result.Succeeded;
		}

		public async Task<bool> RejectFriend(int id)
		{
			Friend friendRequest = await _unitOfWork.Repository<Friend>().GetByIdAsync(id);

			_unitOfWork.Repository<Friend>().Delete(friendRequest);

			return await _unitOfWork.Complete();
		}


		private Friend CreateFriendForFriend(Friend friendRequest)
		{
			return new Friend
			{
				UserId = friendRequest.FriendId,
				UserEmail = friendRequest.FriendEmail,
				UserFirstName = friendRequest.FriendFirstName,
				UserLastName = friendRequest.FriendLastName,
				BirthDateUser = friendRequest.FriendBirthDate,
				FriendId = friendRequest.UserId,
				FriendFirstName = friendRequest.UserFirstName,
				FriendLastName = friendRequest.FriendLastName,
				FriendBirthDate = friendRequest.BirthDateUser,
				FriendEmail = friendRequest.UserEmail,
				FriendStatus = FriendStatus.Accepted
			};
		}
	}
}
