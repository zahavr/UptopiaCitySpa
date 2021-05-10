using API.Dto;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using System;

namespace API.Helpers.Resolvers
{
	public class AvatarUrlResolver : IValueResolver<User, UserCabinetDto, string>
	{
		private readonly IBlobService _blobService;

		public AvatarUrlResolver(IBlobService blobService)
		{
			_blobService = blobService;
		}

		public string Resolve(User source, UserCabinetDto destination, string destMember, ResolutionContext context)
		{
			Uri pohotoUrl = _blobService.GetPhoto(source.Email);

			return pohotoUrl.AbsoluteUri;
		}
	}
}
