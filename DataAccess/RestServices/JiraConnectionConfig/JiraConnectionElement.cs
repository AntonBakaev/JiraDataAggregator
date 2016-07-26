using System.Configuration;

namespace DataAccess.RestServices.JiraConnectionConfig
{
	public class JiraConnectionElement : ConfigurationElement
	{
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("baseUrl", IsRequired = true)]
		public string BaseUrl
		{
			get { return (string)this["baseUrl"]; }
			set { this["baseUrl"] = value; }
		}

		[ConfigurationProperty("login", IsRequired = true)]
		public string Login
		{
			get { return (string)this["login"]; }
			set { this["login"] = value; }
		}

		[ConfigurationProperty("password", IsRequired = true)]
		public string Password
		{
			get { return (string)this["password"]; }
			set { this["password"] = value; }
		}
	}
}
