using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Common.Helpers.Interfaces;

namespace Common.Helpers
{
	public class SerializeHelper<T> : ISerializeHelper<T>
	{
		private const string RootName = "executions";

		private const string Amp = "&amp;";
		private const string Quot = "&quot;";

		private const string BadAmpersandRegex = "&(?![a-zA-Z]{2,6};|#[a-zA-Z0-9]{2,4};)";
		private const string BadOpenQuote = "«";
		private const string BadCloseQuote = "»";

		public T DeserializeXml(string filePath)
		{
			string badXml = File.ReadAllText(filePath);
			var ampersandRegex = new Regex(BadAmpersandRegex);
			string goodXml = ampersandRegex.Replace(badXml, Amp)
										   .Replace(BadOpenQuote, Quot)
										   .Replace(BadCloseQuote, Quot);

			var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(RootName));
			using (var reader = new StringReader(goodXml))
			{
				return (T)serializer.Deserialize(reader);
			}
		}

		public void Serialize(string fileNameToGenerate, T objectToSerialize)
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
