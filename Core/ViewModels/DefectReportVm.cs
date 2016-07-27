using System;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	[XmlRoot("defectReport")]
	public class DefectReportVm : IViewModel
	{
		[XmlElement("flowStatistic")]
		public FlowStatisticsVm FlowStatisticsVm { get; set; }

		[XmlElement("retailShopFlowStatistic")]
		public FlowStatisticsVm RetailShopFlowStatisticsVm { get; set; }

		[XmlElement("topBlockingDefects")]
		public BlockingIssuesVm BlockingIssuesVm { get; set; }

		[XmlElement("defects")]
		public AllDefectKeysVm AllDefectKeysVm { get; set; }
	}
}
