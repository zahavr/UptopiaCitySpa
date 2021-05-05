using API.Dto;
using API.Helpers.Resolvers;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Helpers.MapperProfiles
{
	public class UserProfile : Profile
    {
		public UserProfile()
		{
			CreateMap<RegisterDto, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Login));
			CreateMap<User, UserCabinetDto>()
				.ForMember(uc => uc.Age, opt => opt.MapFrom<UserAgeResolver>())
				.ForMember(uc => uc.PictureUrl, opt => opt.MapFrom<AvatarUrlResolver>());
		}
    }
}
