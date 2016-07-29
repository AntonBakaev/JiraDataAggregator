using System;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class IssueVm : IViewModel
	{
		[XmlAttribute("key")]
		public string IssueName { get; set; }

		[XmlAttribute("link")]
		public string Link { get; set; }
	}
}
