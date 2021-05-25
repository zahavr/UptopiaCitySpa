using API.Dto;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.User;
using Core.Interfaces;
using Core.Specification;
using Core.Specification.BuildingSpecification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Presentation
{
	public class PolicePresentation : IPolicePresentation
	{
		private readonly UserManager<User> _userManager;
		private readonly IPoliceService _policeService;
		private readonly IGenericRepository<UserAppartament> _userAppartamentRepository;
		private readonly IGenericRepository<Business> _businessRepository;
		private readonly IGenericRepository<Friend> _friendRepository;
		private readonly IGenericRepository<Violation> _violationRepository;
		private readonly IMapper _mapper;

		public PolicePresentation(
			UserManager<User> userManager,
			IPoliceService policeService,
			IGenericRepository<UserAppartament> userAppartamentRepository,
			IGenericRepository<Business> businessRepository,
			IGenericRepository<Friend> friendRepository,
			IGenericRepository<Violation> violationRepository,
			IMapper mapper
			)
		{
			_userManager = userManager;
			_policeService = policeService;
			_userAppartamentRepository = userAppartamentRepository;
			_businessRepository = businessRepository;
			_friendRepository = friendRepository;
			_violationRepository = violationRepository;
			_mapper = mapper;
		}

		public async Task<TableData<UserCabinetDto>> GetUsers(TablePoliceParams tableParams)
		{
			TableData<UserCabinetDto> tableData = new TableData<UserCabinetDto>();
			IQueryable<User> records = _userManager.Users;

			if (!string.IsNullOrEmpty(tableParams.SortField) && tableParams.SortOrder != 0)
			{
				if (tableParams.SortField.Equals("firstName") && tableParams.SortOrder == 1)
				{
					records = records.OrderBy(u => u.FirstName);
				}
				if (tableParams.SortField.Equals("firstName") && tableParams.SortOrder == -1)
				{
					records = records.OrderByDescending(u => u.FirstName);
				}
			}

			if (tableParams.Filters.Any())
			{
				records = PrepareFilter(records, tableParams);
			}

			tableData.Count = await records.CountAsync();

			if (tableParams.Rows != 0)
			{
				records = records.Skip(tableParams.First).Take(tableParams.Rows);
			}

			tableData.Data = _mapper.Map<List<User>, List<UserCabinetDto>>(await records.ToListAsync());

			return tableData; ;
		}

		public async Task<FullUserInfo> GetUser(string id)
		{
			User user = await _userManager.FindByIdAsync(id);
			FullUserInfo fullUser = _mapper.Map<User, FullUserInfo>(user);

			fullUser.HasBusiness = await _policeService.UserHasBusiness(id);
			fullUser.HasAppartaments = await _policeService.UserHasAppartaments(id);
			fullUser.Roles = await _userManager.GetRolesAsync(user);

			return fullUser;
		}

		public async Task<IReadOnlyList<AppartamentForPoliceDto>> GetUserAppartaments(string userId)
		{
			UserAppartamentsForPoliceSpecification spec = new UserAppartamentsForPoliceSpecification(userId);

			IReadOnlyList<UserAppartament> result = await _userAppartamentRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<UserAppartament>, IReadOnlyList<AppartamentForPoliceDto>>(result);
		}

		public async Task<IReadOnlyList<BusinessDto>> GetUserBusiness(string userId)
		{
			UserBusinessForPoliceSpecification spec = new UserBusinessForPoliceSpecification(userId);

			IReadOnlyList<Business> result = await _businessRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Business>, IReadOnlyList<BusinessDto>>(result);
		}

		public async Task<IReadOnlyList<UserFriendViewDto>> GetUserFriends(BaseSpecParams baseParams, string userId)
		{
			UserFriendsForPoliceSpecification spec = new UserFriendsForPoliceSpecification(baseParams, userId);

			IReadOnlyList<Friend> result = await _friendRepository.ListAsync(spec);

			return _mapper.Map<IReadOnlyList<Friend>, IReadOnlyList<UserFriendViewDto>>(result);
		}

		public async Task<bool> SetViolation(ViolationDto violationDto, ClaimsPrincipal user)
		{
			Violation violation = _mapper.Map<ViolationDto, Violation>(violationDto);

			User policeman = await _userManager.FindByEmailFromClaimsPrincipals(user);

			return await _policeService.SetViolation(violation, policeman);
		}

		public async Task<TableData<ViolationViewDto>> GetUserViolations(TablePoliceParams tableParams, string userId)
		{
			CountUserViolationSpecification countSpec = new CountUserViolationSpecification(userId);
			UserViolationWithOrdersSpecification dataSpec = new UserViolationWithOrdersSpecification(tableParams, userId);

			int countData = await _violationRepository.CountAsync(countSpec);

			IReadOnlyList<Violation> violations = await _violationRepository.ListAsync(dataSpec);
			IReadOnlyList<ViolationViewDto> data = _mapper.Map<IReadOnlyList<Violation>, IReadOnlyList<ViolationViewDto>>(violations);

			return new TableData<ViolationViewDto> { Data = data, Count = countData };
		}


		public async Task<bool> AmnestyUser(int amnestyId)
		{
			Violation violation = await _violationRepository.GetByIdAsync(amnestyId);

			return await _policeService.AmnestyUser(violation);
		}

		private IQueryable<User> PrepareFilter(IQueryable<User> records, TablePoliceParams tableParams)
		{
			foreach (TableFilterItem filter in tableParams.Filters)
			{
				if (string.Equals(filter.Field, "firstName", StringComparison.OrdinalIgnoreCase))
				{
					records = records.Where(x => x.FirstName.Contains(filter.Value));
				}
				if (string.Equals(filter.Field, "lastName", StringComparison.OrdinalIgnoreCase))
				{
					records = records.Where(x => x.LastName.Contains(filter.Value));
				}
				if (string.Equals(filter.Field, "email", StringComparison.OrdinalIgnoreCase))
				{
					records = records.Where(x => x.Email.Contains(filter.Value));
				}
				if (string.Equals(filter.Field, "dateRange", StringComparison.OrdinalIgnoreCase))
				{
					IEnumerable<DateTime> dates = filter.Value.Split('-', StringSplitOptions.RemoveEmptyEntries)
						.Select(x => DateTime.Parse(x));

					records = records.Where(x => x.BirthDate > dates.First() && x.BirthDate < dates.Last());
				}
				if (string.Equals(filter.Field, "phoneNumber", StringComparison.OrdinalIgnoreCase))
				{
					records = records.Where(x => x.PhoneNumber.Contains(filter.Value));
				}
			}

			return records;
		}
	}
}
