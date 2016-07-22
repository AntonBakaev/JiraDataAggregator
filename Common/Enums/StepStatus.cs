using System.Xml.Serialization;

namespace Common.Enums
{
	public enum StepStatus
	{
		[XmlEnum("")]
		Undefined,

		[XmlEnum("PASS")]
		Pass,

		[XmlEnum("FAIL")]
		Fail,

		[XmlEnum("UNEXECUTED")]
		Unexecuted,

		[XmlEnum("BLOCKED")]
		Blocked
	}
}
