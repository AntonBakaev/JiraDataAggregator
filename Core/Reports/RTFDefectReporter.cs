using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.ViewModels;
using Common.Helpers;

namespace Core.Reports
{
    public class RtfDefectReporter : RtfReporter<DefectReportVm>
    {
        private const string RtfReportFileConfigKey = "RtfDefectReportFileName";

        public void Generate(DefectReportVm defectReportVm)
        {
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
                blockingIssuesBuilder.AppendLine(string.Format("\n• {0} - blocks {1} flows\n", defect.DefectName, defect.BlockingIssuesCount));

                foreach (IssueVm issue in defect.BlockingIssues)
                {
                    blockingIssuesBuilder.AppendLine("∘ " + string.Format("{0} {1} ", issue.IssueName, issue.Link));
                }
            }

            StringBuilder allDefectKeysStr = new StringBuilder();

            foreach (string defectKey in defectReportVm.AllDefectKeysVm.AllDefectKeys)
            {
                allDefectKeysStr.AppendLine("\t • " + defectKey);
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

            rtbBox.SelectionFont = new Font("Arial", 11);

            start = rtbBox.TextLength;
            line = "\nList of all defects that blocks E2E flow\n\n";

            rtbBox.AppendText(line);
            rtbBox.Select(start, line.Length);

            rtbBox.SelectionFont = new Font("Arial", 13, FontStyle.Bold);

            start = rtbBox.TextLength;
            line = allDefectKeysStr.ToString();
            rtbBox.AppendText(line);
            rtbBox.Select(start, line.Length);

            rtbBox.SelectionFont = new Font("Arial", 11);

            string filePath = ConfigurationManager.AppSettings[RtfReportFileConfigKey];

            if (!File.Exists(filePath))
            {
                FileStream stream = File.Create(filePath);
                stream.Close();
            }

            rtbBox.SaveFile(filePath, RichTextBoxStreamType.RichText);
        }
    }
}
