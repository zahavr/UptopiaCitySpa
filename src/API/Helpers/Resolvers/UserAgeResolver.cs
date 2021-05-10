using API.Dto;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Helpers.Resolvers
{
	public class UserAgeResolver : IValueResolver<User, UserCabinetDto, int>
	{
		public int Resolve(User source, UserCabinetDto destination, int destMember, ResolutionContext context)
		{
			return destination.CalculateAge(source.BirthDate);
		}
	}
}
