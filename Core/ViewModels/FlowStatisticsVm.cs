using System;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	[XmlType("flowStatistic")]
	public class FlowStatisticsVm
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
