using API.Dto;
using AutoMapper;
using Core.Entities.User;
using Core.Interfaces;
using System;

namespace API.Helpers.Resolvers
{
	public class FriendAvatarResolver : IValueResolver<Friend, UserFriendViewDto, string>
	{
		private readonly IBlobService _blobService;
	
		public FriendAvatarResolver(IBlobService blobService)
		{
			_blobService = blobService;
		}
	
		public string Resolve(Friend source, UserFriendViewDto destination, string destMember, ResolutionContext context)
		{
			Uri pohotoUrl = _blobService.GetPhoto(source.FriendEmail);
	
			return pohotoUrl.AbsoluteUri;
		}
	}
}
