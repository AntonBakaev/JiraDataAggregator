using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraDataAggregator._Configuration_.RestService
{
	public class RestServicesSection : ConfigurationSection
	{
		[ConfigurationProperty("services", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(RestServicesCollection), AddItemName = "add")]
		public RestServicesCollection Services
		{
			get { return (RestServicesCollection)base["services"]; }
		}
	}
}
