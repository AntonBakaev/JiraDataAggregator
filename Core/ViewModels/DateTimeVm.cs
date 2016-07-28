using System;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DateTimeVm
	{
		[XmlElement("generatedDateTime")]
		public string Value { get; set; }
	}
}
