using System;
using System.Globalization;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	[XmlRoot("defectReport")]
	public class DefectReportVm : IViewModel
	{
		private string generatedDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm", CultureInfo.CurrentCulture);

		[XmlElement("generatedDateTime")]
		public string GeneratedDateTime { get { return generatedDateTime; } set { generatedDateTime = value; } }

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
