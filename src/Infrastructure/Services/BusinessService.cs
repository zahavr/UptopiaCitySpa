using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class BusinessService : IBusinessService
    {
		private const int PRICE_FOR_ONE_WORKER = 100;

		private readonly IUnitOfWork _unitOfWork;

		public BusinessService(
			IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public bool IsHasMoneyForOpenBusiness(User user, int maxCountOfWorker)
		{
			int price = maxCountOfWorker * PRICE_FOR_ONE_WORKER;

			return user.Money > price;
		}

		public async Task<bool> CreateBusinessRequest(Business business)
		{
			_unitOfWork.Repository<Business>().Add(business);

			return await _unitOfWork.Complete();
		}

		public Task<bool> AcceptBusinessRequest(Business business)
		{
			business.BusinessStatus = BusinessStatus.Confirmed;
			business.DateConfirmation = DateTime.Now;
			_unitOfWork.Repository<Business>().Update(business);

			return _unitOfWork.Complete();
		}

		public Task<bool> RejectBusinessRequest(Business business, RejectedApplications rejectedApplications)
		{
			_unitOfWork.Repository<Business>().Delete(business);
			_unitOfWork.Repository<RejectedApplications>().Add(rejectedApplications);

			return _unitOfWork.Complete();
		}

		public Task<bool> CreateVacansyForBusiness(Vacancy vacancy)
		{
			_unitOfWork.Repository<Vacancy>().Add(vacancy);

			return _unitOfWork.Complete();
		}

		public Task<bool> RespondVacancy(VacancyApplications vacancyApplications)
		{
			_unitOfWork.Repository<VacancyApplications>().Add(vacancyApplications);

			return _unitOfWork.Complete();
		}
	}
}
