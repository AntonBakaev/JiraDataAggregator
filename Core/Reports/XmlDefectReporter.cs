using System.Configuration;
using Common.Helpers.Interfaces;
using Core.ViewModels;

namespace Core.Reports
{
	public class XmlDefectReporter : XmlReporterBase<DefectReportVm>
	{
		private const string XmlReportFileConfigKey = "XmlDefectReportFileName";

		public override string FileName { get { return ConfigurationManager.AppSettings[XmlReportFileConfigKey]; } }

		public XmlDefectReporter(ISerializeHelper<DefectReportVm> serializeHelper) : base(serializeHelper)
		{ }
	}
}
