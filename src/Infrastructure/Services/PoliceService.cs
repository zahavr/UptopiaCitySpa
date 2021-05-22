using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class PoliceService : IPoliceService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PoliceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> AmnestyUser(Violation violation)
		{
			_unitOfWork.Repository<Violation>().Delete(violation);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> SetViolation(Violation violation, User policeman)
		{
			violation.PolicemanId = policeman.Id;
			violation.SetDate = DateTime.Now;

			_unitOfWork.Repository<Violation>().Add(violation);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> UserHasAppartaments(string userId)
		{
			UserAppartament appartaments = await _unitOfWork.Repository<UserAppartament>()
															.GetAll()
															.FirstOrDefaultAsync(a => a.UserId == userId);

			return appartaments != null;													  
		}

		public async Task<bool> UserHasBusiness(string userId)
		{
			Business business = await _unitOfWork.Repository<Business>()
												 .GetAll()
												 .FirstOrDefaultAsync(b => b.OwnerId == userId && b.BusinessStatus == BusinessStatus.Confirmed);

			return business != null;
		}
	}
}
