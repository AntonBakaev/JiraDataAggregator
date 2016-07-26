using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraDataAggregator._Configuration_.RestService
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
