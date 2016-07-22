using System.Xml.Serialization;

namespace Common.Enums
{
	public enum ComponentType
	{
		[XmlEnum("")]
		Undefined,
		[XmlEnum("OSE FE")]
		OseFe,
		[XmlEnum("OSE BE")]
		OseBe
	}
}
