using System;
using System.Collections.Generic;
using Core.Reports.Interfaces;
using Core.ViewModels;

namespace Core.Reports
{
	public class AutomaticRtfDefectReporter : AutomaticRtfReporterBase<DefectReportVm>, IRtfDefectReporter
	{
		public AutomaticRtfDefectReporter(InputFormatter inputFormatter) : base(inputFormatter)
		{
		}

		protected override string BasePath
		{
			get { return @"Templates\{0}Template.rtf"; }
		}

		protected override Dictionary<Type, string> TemplatesLocations
		{
			get
			{
				return new Dictionary<Type, string>
				{
					{typeof (IssueVm), "IssueVm"},
					{typeof (AllDefectKeysVm), "AllDefectKeysVm"},
					{typeof (BlockingIssuesVm), "BlockingIssuesVm"},
					{typeof (DefectKeyVm), "DefectKeyVm"},
					{typeof (DefectVm), "DefectVm"},
					{typeof (FlowStatisticsVm), "FlowStatisticsVm"},
					{typeof (DefectReportVm), "DefectReportVm"},
				};
			}
		}
	}
}
