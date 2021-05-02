using API.Dto;
using AutoMapper;
using Core.Entities.Identity;

namespace API.Helpers.MapperProfiles
{
	public class UserProfile : Profile
    {
		public UserProfile()
		{
			CreateMap<RegisterDto, User>();
		}
    }
}
