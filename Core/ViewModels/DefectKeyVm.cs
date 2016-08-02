using System;
using System.Xml.Serialization;
using Core.Enums;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectKeyVm : IViewModel
	{
		[XmlAttribute("value")]
		public string Value { get; set; }

		[XmlAttribute("assignee")]
		public string Assignee { get; set; }

		[XmlAttribute("summary")]
		public string Summary { get; set; }

		[XmlAttribute("severity")]
		public IssueSeverity Severity { get; set; }

		[XmlAttribute("components")]
		public string Components { get; set; }

		[XmlAttribute("status")]
		public string Status { get; set; }
	}
}
