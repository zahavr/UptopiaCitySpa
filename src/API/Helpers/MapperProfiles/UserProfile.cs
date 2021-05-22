using API.Dto;
using API.Helpers.Resolvers;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.User;

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
				.ForMember(uc => uc.PictureUrl, opt => opt.MapFrom<AvatarUrlResolver>())
				.ReverseMap();

			CreateMap<BaseUserDto, User>();
			CreateMap<User, FullUserInfo>()
				.ForMember(fu => fu.PictureUrl, opt => opt.MapFrom<UserForPoliceResolver>());

			CreateMap<User, UserFriendViewDto>()
				.ForMember(uf => uf.Id, opt => opt.Ignore())
				.ForMember(uf => uf.FriendId, opt => opt.MapFrom(u => u.Id))
				.ForMember(uf => uf.FriendFirstName, opt => opt.MapFrom(u => u.FirstName))
				.ForMember(uf => uf.FriendLastName, opt => opt.MapFrom(u => u.LastName))
				.ForMember(uf => uf.FriendEmail, opt => opt.MapFrom(u => u.Email))
				.ForMember(uf => uf.FriendBirthDate, opt => opt.MapFrom(u => u.BirthDate))
				.ForMember(uf => uf.ProfileImgUrl, opt => opt.MapFrom<SearchAvatarResolver>());

			CreateMap<UserFriendViewDto, Friend>()
				.ForMember(x => x.Id, opt => opt.Ignore());

			CreateMap<Friend, UserFriendViewDto>()
				.ForMember(uf => uf.ProfileImgUrl, opt => opt.MapFrom<FriendAvatarResolver>());

			CreateMap<User, Friend>()
				.ForMember(f => f.Id, opt => opt.Ignore())
				.ForMember(f => f.UserId, opt => opt.MapFrom(u => u.Id))
				.ForMember(f => f.UserFirstName, opt => opt.MapFrom(u => u.FirstName))
				.ForMember(f => f.UserLastName, opt => opt.MapFrom(u => u.LastName))
				.ForMember(f => f.BirthDateUser, opt => opt.MapFrom(u => u.BirthDate))
				.ForMember(f => f.UserEmail, opt => opt.MapFrom(u => u.Email));

			CreateMap<RoleDto, Role>();
		}
    }
}
