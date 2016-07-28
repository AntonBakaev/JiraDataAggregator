using System.Collections.Generic;
using System.Linq;
using TemplateReplacer.Common;

namespace TemplateReplacer.Helpers
{
	internal class PropertyDataHelper
	{
		public static PropertyData GetPropertyByName(List<PropertyData> data, string propName)
		{
			return data.FirstOrDefault(p => p.PropertyName == propName);
		}
	}
}
