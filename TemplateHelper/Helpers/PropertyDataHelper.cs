using System.Collections.Generic;
using System.Linq;
using TemplateHelper.Common;

namespace TemplateHelper.Helpers
{
	internal class PropertyDataHelper
	{
		public static PropertyData GetPropertyByName(List<PropertyData> data, string propName)
		{
			return data.FirstOrDefault(p => p.PropertyName == propName);
		}
	}
}
