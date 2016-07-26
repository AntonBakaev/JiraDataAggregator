using System.Configuration;

namespace JiraDataAggregator._Configuration_.RestService
{
	public class RestServicesCollection : ConfigurationElementCollection
	{
		public RestServiceElement this[int index]
		{
			get { return (RestServiceElement)base.BaseGet(index); }
			set
			{
				if (base.BaseGet(index) != null)
					base.BaseRemoveAt(index);
				base.BaseAdd(index, value);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new RestServiceElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RestServiceElement)element).Name;
		}
	}
}
