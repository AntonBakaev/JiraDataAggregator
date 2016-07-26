using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Helpers
{
    class ReportGeneratorHelper
    {
        public static string GenerateRtfReport(string template, IDictionary<string, string> parameters = null)
        {
            var requestParameters = parameters != null
                ? parameters.ToDictionary(p => p.Key, p => p.Value)
                : new Dictionary<string, string>();
            foreach (Match match in Regex.Matches(template, @"\\\{(.*?)\\\}"))
            {
                var paramName = match.Groups[1].Value;
                if (!requestParameters.ContainsKey(paramName))
                {
                    throw new ArgumentException(string.Format("'{0}' must be defined to generate url", paramName), "parameters");
                }

                var paramValue = requestParameters[paramName];
                if (paramValue == null)
                {
                    throw new ArgumentException(string.Format("'{0}' must not be 'null'", paramName), "parameters");
                }

                template = template.Replace(string.Format("\\{{{0}\\}}", paramName), paramValue);
                requestParameters.Remove(paramName);
            }

            return template;
        }
    }
}
