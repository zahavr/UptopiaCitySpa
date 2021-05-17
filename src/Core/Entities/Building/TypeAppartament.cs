using System.Runtime.Serialization;

namespace Core.Entities
{
	public enum TypeAppartament
    {
        [EnumMember(Value = "Econom")]
        Econom,
        [EnumMember(Value = "Comfort")]
        Comfort,
        [EnumMember(Value = "Luxe")]
        Luxe 
    }
}
