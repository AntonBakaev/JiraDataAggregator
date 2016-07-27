using System.Configuration;
using Core.ViewModels;

namespace Core.Reports
{
	public class XmlDefectReporter : XmlReporter<DefectReportVm>
	{
		private const string XmlReportFileConfigKey = "XmlDefectReportFileName";

		public void Generate(DefectReportVm defectReportVm)
		{
			base.Generate(ConfigurationManager.AppSettings[XmlReportFileConfigKey], defectReportVm);
		}
	}
}
