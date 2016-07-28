using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.ViewModels;
using Common.Helpers;

namespace Core.Reports
{
    public class RtfDefectReporter : RtfReporter<DefectReportVm>
    {
        private const string RtfReportFileConfigKey = "RtfDefectReportFileName";

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
				//parameters.Remove(paramName);
			}

			return template;
		}

		public void Generate(DefectReportVm defectReportVm)
		{
			string filePath = ConfigurationManager.AppSettings[RtfReportFileConfigKey];

			Dictionary<string, string> links = new Dictionary<string, string>();

			StringBuilder flowStatisticsStr = new StringBuilder();

			// first piece
			flowStatisticsStr.AppendLine("\t • Passed: " + defectReportVm.FlowStatisticsVm.Passed);
			flowStatisticsStr.AppendLine("\t • Failed: " + defectReportVm.FlowStatisticsVm.Failed);
			flowStatisticsStr.AppendLine("\t • Wip: " + defectReportVm.FlowStatisticsVm.Wip);
			flowStatisticsStr.AppendLine("\t • Blocked: " + defectReportVm.FlowStatisticsVm.Blocked);

			StringBuilder retailShopFlowStatisticsStr = new StringBuilder();
			// first piece
			retailShopFlowStatisticsStr.AppendLine("\t • Passed: " + defectReportVm.FlowStatisticsVm.Passed);
			retailShopFlowStatisticsStr.AppendLine("\t • Failed: " + defectReportVm.FlowStatisticsVm.Failed);
			retailShopFlowStatisticsStr.AppendLine("\t • Wip: " + defectReportVm.FlowStatisticsVm.Wip);
			retailShopFlowStatisticsStr.AppendLine("\t • Blocked: " + defectReportVm.FlowStatisticsVm.Blocked);

			StringBuilder blockingIssuesBuilder = new StringBuilder();

			foreach (DefectVm defect in defectReportVm.BlockingIssuesVm.DefectsList)
			{
				blockingIssuesBuilder.AppendLine(string.Format("\t • {0} - blocks {1} flows", defect.DefectName, defect.BlockingIssuesCount));

				foreach (IssueVm issue in defect.BlockingIssues)
				{
					blockingIssuesBuilder.AppendLine("\t\t •" + string.Format("{{{0}}}", issue.Link));

					if (!links.ContainsKey(issue.Link))
						links.Add(issue.Link, issue.IssueName);
				}
			}

			StringBuilder allDefectKeysStr = new StringBuilder();

			foreach (string defectKey in defectReportVm.AllDefectKeysVm.AllDefectKeys)
			{
				allDefectKeysStr.AppendLine("\t •" + defectKey);
			}

			RichTextBox rtbBox = new RichTextBox();

			int start = rtbBox.TextLength;
			string line = "Defect report - Phase 1\n";
			rtbBox.AppendText(line);

			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 15, FontStyle.Bold);
			rtbBox.SelectionAlignment = HorizontalAlignment.Center;

			start = rtbBox.TextLength;
			line = "(No Jira integration)\n\n";
			rtbBox.AppendText(line);

			rtbBox.Select(start, line.Length);

			rtbBox.SelectionFont = new Font("Arial", 13);
			rtbBox.SelectionAlignment = HorizontalAlignment.Center;

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
			//rtbBoxTest.Rtf = @"{\colortbl ;\red0\green0\blue238;}{\field{\*\fldinst HYPERLINK 'URL'}{\fldrslt{\ul\cf1Text to display}}}";

			//string text = rtbBox.Rtf;

			//text = text.Remove(text.Length - 2, 1);
			//text += "\n";
			//text += @" {\colortbl ;\red0\green0\blue238;} {\field{\*\fldinst HYPERLINK ""URL""}{\fldrslt{\ul\cf1Text to display}}} ";
			//text += "}";

			//RichTextBox rtb = new RichTextBox();
			//rtb.Rtf = text;

			//	@"{\rtf1\ansi\ {\colortbl ;\red0\green0\blue238;} {\field{\*\fldinst HYPERLINK ""URL""}{\fldrslt{\ul\cf1Text to display}}}}";

			//StringBuilder append = new StringBuilder();
			//append.Append(@"{\rtf1\ansi\deff3\adeflang1025 {{\field{\*\fldinst HYPERLINK """);
			//append.Append("http://www.google.com");
			//append.Append(@""" }{{");
			//append.Append("google.com");
			//append.Append(@"} }}}}}");

			//string rtf = append.ToString();

			//string rtf = rtbBoxTest.Rtf;

			// rtbBoxTest.Rtf = rtf;

			if (!File.Exists(filePath))
			{
				FileStream stream = File.Create(filePath);
				stream.Close();
			}

			rtbBox.SaveFile(filePath, RichTextBoxStreamType.RichText);
			string template = File.ReadAllText(filePath);

			//Dictionary<string, string> dic = new Dictionary<string, string>();
			//dic.Add(@"Hallo", @" {\colortbl ;\red0\green0\blue238;} {\field{\*\fldinst HYPERLINK ""URL""}{\fldrslt{\ul\cf1Text to display}}} ");

			File.WriteAllText(filePath, GenerateRtfReport(template, links));
		}
    }
}
