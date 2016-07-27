using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Common.Helpers
{
	public static class SerializeHelper<T> where T : new()
	{
		private const string RootName = "executions";

		private const string Amp = "&amp;";
		private const string Quot = "&quot;";

		private const string BadAmpersandRegex = "&(?![a-zA-Z]{2,6};|#[a-zA-Z0-9]{2,4};)";
		private const string BadOpenQuote = "«";
		private const string BadCloseQuote = "»";

		public static List<T> DeserializeXml(string filePath)
		{
			string badXml = File.ReadAllText(filePath);
			var ampersandRegex = new Regex(BadAmpersandRegex);
			string goodXml = ampersandRegex.Replace(badXml, Amp)
										   .Replace(BadOpenQuote, Quot)
										   .Replace(BadCloseQuote, Quot);

			var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(RootName));
			using (var reader = new StringReader(goodXml))
			{
				return (List<T>)serializer.Deserialize(reader);
			}
		}

		public static void Serialize(string fileNameToGenerate, object objectToSerialize)
		{
			var serializer = new XmlSerializer(typeof(T));
			File.Delete(fileNameToGenerate);
			using (var stream = new FileStream(fileNameToGenerate, FileMode.OpenOrCreate))
			{
				serializer.Serialize(stream, objectToSerialize);
			}
		}
	}
}
