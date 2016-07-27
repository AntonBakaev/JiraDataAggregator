using System;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class IssueVm
	{
		[XmlAttribute("key")]
		public string IssueName { get; set; }

		[XmlAttribute("link")]
		public string Link { get; set; }
	}
}
