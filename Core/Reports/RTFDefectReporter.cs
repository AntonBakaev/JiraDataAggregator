using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Exceptions;
using Common.Messages;
using Core.Reports.Interfaces;
using Core.ViewModels;

namespace Core.Reports
{
	public class RtfDefectReporter : RtfReporterBase<DefectReportVm>, IRtfDefectReporter
	{
		protected override string RtfReportFileConfigKey
		{
			get { return "RtfDefectReportFileName"; }
		}

		public static string GenerateRtfReport(string template, IDictionary<string, string> parameters)
		{
			foreach (Match match in Regex.Matches(template, @"\\\{(.*?)\\\}"))
			{
				var paramName = match.Groups[1].Value;
				if (!parameters.ContainsKey(paramName))
				{
					throw new ArgumentException(string.Format("'{0}' must be defined", paramName), "parameters");
				}

				var paramValue = parameters[paramName];
				if (paramValue == null)
				{
					throw new ArgumentException(string.Format("'{0}' must not be 'null'", paramName), "parameters");
				}

				paramValue = @" {\colortbl ;\red0\green0\blue238;} {\field{\*\fldinst HYPERLINK """
							 + paramName + @"""}{\fldrslt{\ul\cf1" + paramValue + "}}} ";

				template = template.Replace(string.Format("\\{{{0}\\}}", paramName), paramValue);
			}

			return template;
		}

		public override void Generate(DefectReportVm defectReportVm)
		{
			string filePath = ConfigurationManager.AppSettings[RtfReportFileConfigKey];

			var links = new Dictionary<string, string>();

			var flowStatisticsStr = new StringBuilder();

			// first piece
			flowStatisticsStr.AppendLine("\t • Passed: " + defectReportVm.FlowStatisticsVm.Passed);
			flowStatisticsStr.AppendLine("\t • Failed: " + defectReportVm.FlowStatisticsVm.Failed);
			flowStatisticsStr.AppendLine("\t • Wip: " + defectReportVm.FlowStatisticsVm.Wip);
			flowStatisticsStr.AppendLine("\t • Blocked: " + defectReportVm.FlowStatisticsVm.Blocked);

			var retailShopFlowStatisticsStr = new StringBuilder();
			// first piece
			retailShopFlowStatisticsStr.AppendLine("\t • Passed: " + defectReportVm.FlowStatisticsVm.Passed);
			retailShopFlowStatisticsStr.AppendLine("\t • Failed: " + defectReportVm.FlowStatisticsVm.Failed);
			retailShopFlowStatisticsStr.AppendLine("\t • Wip: " + defectReportVm.FlowStatisticsVm.Wip);
			retailShopFlowStatisticsStr.AppendLine("\t • Blocked: " + defectReportVm.FlowStatisticsVm.Blocked);

			var blockingIssuesBuilder = new StringBuilder();

			foreach (DefectVm defect in defectReportVm.BlockingIssuesVm.DefectsList)
			{
				blockingIssuesBuilder.AppendLine(string.Format("\t • {{{0}}} - blocks {1} flows", defect.Link,
					defect.BlockingIssuesCount));

				if (!links.ContainsKey(defect.Link))
					links.Add(defect.Link, defect.DefectName);

				foreach (IssueVm issue in defect.BlockingIssues)
				{
					blockingIssuesBuilder.AppendLine("\t\t •" + string.Format("{{{0}}}", issue.Link));

					if (!links.ContainsKey(issue.Link))
						links.Add(issue.Link, issue.IssueName);
				}
			}

			var allDefectKeysStr = new StringBuilder();

			foreach (DefectKeyVm defectKey in defectReportVm.AllDefectKeysVm.AllDefectKeys)
			{
				allDefectKeysStr.AppendLine("\t •" + defectKey.Value);
			}

			var rtbBox = new RichTextBox();

			int start = rtbBox.TextLength;
			string line = "Defect report\n\n";
			rtbBox.AppendText(line);

			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 15, FontStyle.Bold);
			rtbBox.SelectionAlignment = HorizontalAlignment.Center;

			start = rtbBox.TextLength;

			line = "Generated: " + defectReportVm.GeneratedDateTime + "\n";

			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);

			rtbBox.Select(start, line.Length + 2);

			rtbBox.SelectionFont = new Font("Arial", 13);

			start = rtbBox.TextLength;
			line = "\nOneScreen Flow statistics\n\n";

			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

			start = rtbBox.TextLength;
			line = flowStatisticsStr.ToString();
			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);

			//start = rtbBox.TextLength;
			//line = "\nDate.Time\n\n";

			//rtbBox.AppendText(line);
			//rtbBox.Select(start, line.Length);

			//rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

			start = rtbBox.TextLength;
			line = "\nRetail shop flow statistics\n\n";

			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

			start = rtbBox.TextLength;
			line = retailShopFlowStatisticsStr.ToString();
			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);

			start = rtbBox.TextLength;
			line = "\nTop 10\n\n";

			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

			start = rtbBox.TextLength;
			line = blockingIssuesBuilder.ToString();
			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);

			start = rtbBox.TextLength;
			line = "\nList of all defects that blocks E2E flow\n\n";

			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

			start = rtbBox.TextLength;
			line = allDefectKeysStr.ToString();
			rtbBox.AppendText(line);
			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);

			try
			{
				File.Delete(filePath);
				FileStream stream = File.Create(filePath);
				stream.Close();

				rtbBox.SaveFile(filePath, RichTextBoxStreamType.RichText);
				string template = File.ReadAllText(filePath);

				File.WriteAllText(filePath, GenerateRtfReport(template, links));
			}
			catch (IOException)
			{
				throw new JiraDataAggregatorException(
					string.Format("{0} at {1}", 
					JiraDataAggregatorExceptionMessages.FileExceptionMessages.WriteToFileError, filePath));
			}
			
		}
	}
}
