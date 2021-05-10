using API.Dto;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using System;

namespace API.Helpers.Resolvers
{
	public class SearchAvatarResolver : IValueResolver<User, UserFriendViewDto, string>
	{
		private readonly IBlobService _blobService;

		public SearchAvatarResolver(IBlobService blobService)
		{
			_blobService = blobService;
		}

		public string Resolve(User source, UserFriendViewDto destination, string destMember, ResolutionContext context)
		{
			Uri pohotoUrl = _blobService.GetPhoto(source.Email);

			return pohotoUrl.AbsoluteUri;
		}
	}

}
