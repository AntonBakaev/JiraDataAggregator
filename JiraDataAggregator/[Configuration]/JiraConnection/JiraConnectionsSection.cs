using System.Configuration;

namespace JiraDataAggregator._Configuration_.JiraConnection
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
