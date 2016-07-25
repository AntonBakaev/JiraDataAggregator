using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Core.Models;

namespace Common.Helpers
{
	public static class SerializeHelper
	{
		public const string RootName = "executions";

		public const string Amp = "&amp;";
		public const string Quot = "&quot;";

		public const string BadAmpersandRegex = "&(?![a-zA-Z]{2,6};|#[a-zA-Z0-9]{2,4};)";
		public const string BadOpenQuote = "«";
		public const string BadCloseQuote = "»";

		public static List<Execution> DeserializeXml(string filePath)
		{
			string badXml = File.ReadAllText(filePath);
			var ampersandRegex = new Regex(BadAmpersandRegex);
			string goodXml = ampersandRegex.Replace(badXml, Amp)
										   .Replace(BadOpenQuote, Quot)
										   .Replace(BadCloseQuote, Quot);

			var serializer = new XmlSerializer(typeof(List<Execution>), new XmlRootAttribute(RootName));
			using (var reader = new StringReader(goodXml))
			{
				return (List<Execution>)serializer.Deserialize(reader);
			}
		}
	}
}
