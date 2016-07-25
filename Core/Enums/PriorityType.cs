using System.Xml.Serialization;

namespace Core.Enums
{
	public enum PriorityType
	{
		[XmlEnum("")]
		Undefined,

		[XmlEnum("None")]
		None,

		[XmlEnum("P1")]
		P1,

		[XmlEnum("P2")]
		P2,

		[XmlEnum("P3")]
		P3,

		[XmlEnum("P4")]
		P4,

		[XmlEnum("P5")]
		P5,

		[XmlEnum("P6")]
		P6,

		[XmlEnum("P7")]
		P7
	}
}
