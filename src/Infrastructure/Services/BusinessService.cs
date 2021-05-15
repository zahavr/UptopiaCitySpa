using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<bool> AcceptBusinessRequest(Business business)
		{
			business.BusinessStatus = BusinessStatus.Confirmed;
			business.DateConfirmation = DateTime.Now;
			_unitOfWork.Repository<Business>().Update(business);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> RejectBusinessRequest(Business business, RejectedApplications rejectedApplications)
		{
			_unitOfWork.Repository<Business>().Delete(business);
			_unitOfWork.Repository<RejectedApplications>().Add(rejectedApplications);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> CreateVacancyForBusiness(Vacancy vacancy)
		{
			_unitOfWork.Repository<Vacancy>().Add(vacancy);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> RespondVacancy(VacancyApplications vacancyApplication)
		{
			List<VacancyApplications> vacancyApplications = await _unitOfWork.Repository<VacancyApplications>()
																.GetAll()
																.Where(va => va.ApplicantId == va.ApplicantId)
																.ToListAsync();

			if (vacancyApplications.FirstOrDefault(va => va.ApplicantId == vacancyApplication.ApplicantId
					&& va.VacancyId == vacancyApplication.VacancyId) != null)
			{
				return false;
			}

			_unitOfWork.Repository<VacancyApplications>().Add(vacancyApplication);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> AcceptWorker(VacancyApplications vacancy)
		{
			BusinessWorker worker = new BusinessWorker
			{
				PositionAtWork = vacancy.Vacancy.Title,
				WorkerId = vacancy.ApplicantId,
				StartWork = DateTime.Now,
				Salary = vacancy.Vacancy.Salary,
				BusinessId = vacancy.Vacancy.BusinessId
			};

			_unitOfWork.Repository<VacancyApplications>().Delete(vacancy);

			_unitOfWork.Repository<BusinessWorker>().Add(worker);

			return await _unitOfWork.Complete();
		}

		public async Task<bool> DismissWorker(BusinessWorker businessWorker)
		{
			_unitOfWork.Repository<BusinessWorker>().Delete(businessWorker);

			return await _unitOfWork.Complete();
		}
	}
}
