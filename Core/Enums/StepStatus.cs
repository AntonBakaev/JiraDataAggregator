using System.Xml.Serialization;

namespace Core.Enums
{
	public enum StepStatus
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
