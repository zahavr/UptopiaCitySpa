using System.Runtime.Serialization;

namespace Core.Entities
{
	public enum BusinessStatus
    {
        [EnumMember(Value = "Denied")]
        Denied,
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Pending")]
        Pending
    }
}
