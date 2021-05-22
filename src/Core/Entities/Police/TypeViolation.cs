using System.Runtime.Serialization;

namespace Core.Entities
{
	public enum TypeViolation
    {
        [EnumMember(Value = "Low")]
        Low = 0,
        [EnumMember(Value = "Medium")]
        Medium = 1,
        [EnumMember(Value = "High")]
        High = 2
    }
}
