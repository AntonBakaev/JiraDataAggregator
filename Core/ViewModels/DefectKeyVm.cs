using System;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectKeyVm
	{
		[XmlAttribute("value")]
		public string Value { get; set; }
	}
}
