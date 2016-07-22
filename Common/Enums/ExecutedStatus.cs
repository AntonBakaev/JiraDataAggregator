using System.Xml.Serialization;

namespace Common.Enums
{
	public enum ExecutedStatus
	{
		[XmlEnum("")]
		Undefined,

		[XmlEnum("PASS")]
		Pass,

		[XmlEnum("FAIL")]
		Fail,

		[XmlEnum("WIP")]
		WIP,

		[XmlEnum("UNEXECUTED")]
		Unexecuted,

		[XmlEnum("BLOCKED")]
		Blocked
	}
}
