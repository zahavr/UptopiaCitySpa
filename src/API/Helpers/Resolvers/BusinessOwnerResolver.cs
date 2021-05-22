using API.Dto;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers.Resolvers
{
	public class BusinessOwnerResolver : IValueResolver<BusinessDto, Business, string>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		public BusinessOwnerResolver(
			IHttpContextAccessor httpContextAccessor,
			UserManager<User> userManager)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public string Resolve(BusinessDto source, Business destination, string destMember, ResolutionContext context)
		{
			User user = _userManager.FindByEmailFromClaimsPrincipals(_httpContextAccessor.HttpContext.User).Result;

			return user.Id;
		}
	}
}
