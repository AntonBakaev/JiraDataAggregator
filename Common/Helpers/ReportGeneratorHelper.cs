using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;

namespace Common.Helpers
{
	public class ReportGeneratorHelper
	{
		public static string GenerateRtfReport(string template, IDictionary<string, string> parameters)
		{
			foreach (Match match in Regex.Matches(template, @"\\\{(.*?)\\\}"))
			{
				var paramName = match.Groups[1].Value;
				if (!parameters.ContainsKey(paramName))
				{
					throw new ArgumentException(String.Format("'{0}' must be defined to generate url", paramName), "parameters");
				}

				var paramValue = parameters[paramName];
				if (paramValue == null)
				{
					throw new ArgumentException(String.Format("'{0}' must not be 'null'", paramName), "parameters");
				}

				template = template.Replace(String.Format("\\{{{0}\\}}", paramName), paramValue);
				parameters.Remove(paramName);
			}

			return template;
		}

		public static void GenerateReport(Dictionary<string, string> allModels)
		{
			string path = ConfigurationManager.ConnectionStrings["TemplatePath"].ConnectionString;
		
			string reportPath = ConfigurationManager.ConnectionStrings["ReportPath"].ConnectionString;

			string template = File.ReadAllText(path);

			string datetime = DateTime.Now.ToString("yyyy-M-d hh:mm");

			allModels.Add("Date.Time", datetime);

			template = GenerateRtfReport(template, allModels);

			File.WriteAllText(reportPath, template);
		}
	}
}
