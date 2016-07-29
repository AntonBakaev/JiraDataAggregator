using System.Configuration;

namespace DataAccess.RestServices.JiraConnectionConfig
{
	public class JiraConnectionsSection : ConfigurationSection
	{
		[ConfigurationProperty("connections", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(JiraConnectionsCollection), AddItemName = "add")]
		public JiraConnectionsCollection Connections
		{
			get { return (JiraConnectionsCollection)base["connections"]; }
		}
	}
}
