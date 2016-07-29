using System.Configuration;

namespace DataAccess.RestServices.JiraConnectionConfig
{
	public class JiraConnectionsCollection : ConfigurationElementCollection
	{
		public JiraConnectionElement this[int index]
		{
			get { return (JiraConnectionElement)base.BaseGet(index); }
			set
			{
				if (base.BaseGet(index) != null)
					base.BaseRemoveAt(index);
				base.BaseAdd(index, value);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new JiraConnectionElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((JiraConnectionElement)element).Name;
		}
	}
}
