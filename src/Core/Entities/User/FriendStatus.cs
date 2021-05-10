using System.Runtime.Serialization;

namespace Core.Entities.User
{
	public enum FriendStatus
    {
        [EnumMember(Value = "Accepted")]
        Accepted,
        [EnumMember(Value = "Pending")]
        Pending
    }
}
