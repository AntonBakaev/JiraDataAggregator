using System.Configuration;

namespace DataAccess.RestServices.RestServiceConfig
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
