using API.Dto;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using System;

namespace API.Helpers.Resolvers
{
	public class UserForPoliceResolver : IValueResolver<User, FullUserInfo, string>
	{
		private readonly IBlobService _blobService;

		public UserForPoliceResolver(IBlobService blobService)
		{
			_blobService = blobService;
		}

		public string Resolve(User source, FullUserInfo destination, string destMember, ResolutionContext context)
		{
			Uri pohotoUrl = _blobService.GetPhoto(source.Email);

			return pohotoUrl.AbsoluteUri;
		}
	}
}
