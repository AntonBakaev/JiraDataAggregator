using System.Xml.Serialization;

namespace Core.Enums
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
