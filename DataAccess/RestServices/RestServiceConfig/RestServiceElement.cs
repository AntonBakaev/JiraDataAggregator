using System.Configuration;

namespace DataAccess.RestServices.RestServiceConfig
{
	public class RestServiceElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("url", IsRequired = true)]
		public string Url
		{
			get { return (string)this["url"]; }
			set { this["url"] = value; }
		}

		[ConfigurationProperty("endPointName", IsRequired = true)]
		public string EndPointName
		{
			get { return (string)this["endPointName"]; }
			set { this["endPointName"] = value; }
		}
	}
}
