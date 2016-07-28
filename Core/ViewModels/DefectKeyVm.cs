using System;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectKeyVm
	{
		[XmlElement("defectKey")]
		public string Value { get; set; }
	}
}
