using System.Runtime.Serialization;

namespace Core.Enums
{
	[DataContract]
	public enum IssueSeverity
	{
		Unknown = 0,
		[EnumMember(Value = "Low")]
		Low,
		[EnumMember(Value = "Medium")]
		Medium,
		[EnumMember(Value = "Critical")]
		Critical
	}
}
