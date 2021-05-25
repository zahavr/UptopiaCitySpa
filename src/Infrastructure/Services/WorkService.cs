using Core;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class WorkService : IWorkService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private const int MINIMUM_WORKHOURS = 8;
		private const int WORKHOURS_WEEK = 40;

		public WorkService(
			IUnitOfWork unitOfWork,
			UserManager<User> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public async Task<bool> StartShift(User user)
		{
			Shift shift = new Shift
			{
				StartShift = DateTime.Now.AddHours(-1),
				UserId = user.Id
			};

			_unitOfWork.Repository<Shift>().Add(shift);

			return await _unitOfWork.Complete();
		}


		public async Task<ResultWithMessage> EndShift(Shift shift)
		{
			ResultWithMessage result = new ResultWithMessage(false);

			shift.EndShift = DateTime.Now;
			double hoursWorked = FindWorkedHours(shift);

			if (hoursWorked < MINIMUM_WORKHOURS)
			{
				result.Message = "You must work 8 hours";
				return result;
			}

			shift.EarnedMoney = await CalculateEarnedMoney(shift, hoursWorked);

			if (!await UpdateUserMoney(shift))
			{
				result.Message = "Cannot update user money";
				return result;
			}

			_unitOfWork.Repository<Shift>().Update(shift);

			result.Message = $"You earned {shift.EarnedMoney}";
			result.IsSuccess = await _unitOfWork.Complete();

			return result;
		}

		private async Task<bool> UpdateUserMoney(Shift shift)
		{
			User user = await _userManager.FindByIdAsync(shift.UserId);
			user.Money += shift.EarnedMoney.Value;

			IdentityResult result = await _userManager.UpdateAsync(user);

			return result.Succeeded;
		}

		private async Task<decimal> CalculateEarnedMoney(Shift shift, double hoursWorked)
		{
			decimal salaryPerHour = await FindSalaryPerHour(shift);

			return Math.Round((decimal)hoursWorked * salaryPerHour, 2);
		}

		private async Task<decimal> FindSalaryPerHour(Shift shift)
		{
			BusinessWorker worker = await _unitOfWork.Repository<BusinessWorker>().GetAll()
																				  .FirstOrDefaultAsync(bw => bw.WorkerId == shift.UserId);

			return (worker.Salary * 12 / 52) / WORKHOURS_WEEK;
		}

		private double FindWorkedHours(Shift shift) =>
			(shift.EndShift - shift.StartShift).Value.Hours;
	}
}
