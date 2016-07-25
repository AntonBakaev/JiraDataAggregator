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

		[XmlEnum("UNEXECUTED")]
		Unexecuted,

		[XmlEnum("BLOCKED")]
		Blocked
	}
}
