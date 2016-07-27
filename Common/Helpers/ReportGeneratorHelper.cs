using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Text;
using Core.ViewModels;

namespace Common.Helpers
{
	public class ReportGeneratorHelper
	{

        public static string GenerateBlockingIssues(BlockingIssuesVm blockingIssues)
        {
            StringBuilder blockingIssuesPart = new StringBuilder();
            foreach (var defect in blockingIssues.DefectsList)
            {
                string defectLine = string.Format("{0} - blocks {1} flows", defect.DefectName, defect.BlockingIssues.Count);
                blockingIssuesPart.Append("\\line \\bullet ");
                foreach (var issue in defect.BlockingIssues)
                {
                    string issueLine = string.Format("{0} {1}", issue.IssueName, issue.Link);

                    blockingIssuesPart.Append("\\line \\tab \\u9632 " + issueLine + "}");
                }
            }
            return blockingIssuesPart.ToString();
        }

        public static string GenerateRtfReport(string template, IDictionary<string, string> parameters)
        {
            foreach (Match match in Regex.Matches(template, @"\\\{(.*?)\\\}"))
            {
                var paramName = match.Groups[1].Value;
                if (!parameters.ContainsKey(paramName))
                {
                    throw new ArgumentException(string.Format("'{0}' must be defined to generate url", paramName), "parameters");
                }

                var paramValue = parameters[paramName];
                if (paramValue == null)
                {
                    throw new ArgumentException(string.Format("'{0}' must not be 'null'", paramName), "parameters");
                }

                template = template.Replace(string.Format("\\{{{0}\\}}", paramName), paramValue);
                parameters.Remove(paramName);
            }

            return template;
        }


        public static void GenerateReport(Dictionary<string, string> allModels)
        {
            string path = ConfigurationManager.ConnectionStrings["TemplatePath"].ConnectionString;
            string report_path = ConfigurationManager.ConnectionStrings["ReportPath"].ConnectionString;

            string template = File.ReadAllText(path);

            string datetime = DateTime.Now.ToString("yyyy-M-d hh:mm");

            allModels.Add("Date.Time", datetime);

            template = GenerateRtfReport(template, allModels);

            File.WriteAllText(report_path, template);
        }
	}
}
