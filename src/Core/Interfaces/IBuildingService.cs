﻿using Core.Entities;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IBuildingService
    {
        Task<bool> AddBuildingAsync(Building building);
		Task<ResultWithMessage> BuyAppartamentsAsync(User user, int appartamentId);
		Task<bool> SellAppartament(User user, UserAppartament appartament);
	}
}
