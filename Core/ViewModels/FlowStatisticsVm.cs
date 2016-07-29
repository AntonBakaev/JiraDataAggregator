using System;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class FlowStatisticsVm : IViewModel
	{
		[XmlElement("passed")]
		public int Passed { get; set; }

		[XmlElement("failed")]
		public int Failed { get; set; }

		[XmlElement("wip")]
		public int Wip { get; set; }

		[XmlElement("blocked")]
		public int Blocked { get; set; }
	}
}
