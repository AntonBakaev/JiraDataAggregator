using System.Runtime.Serialization;

namespace Core.Enums
{
	[DataContract]
	public enum IssueStatus
	{
		Unknown = 0,
		[EnumMember(Value = "Open")]
		Open,
		[EnumMember(Value = "Resolved")]
		Resolved,
		[EnumMember(Value = "Closed")]
		Closed,
		[EnumMember(Value = "Reopened")]
		Reopened,
		[EnumMember(Value = "In progress")]
		InProgress,
		[EnumMember(Value = "Done")]
		Done
	}
}
