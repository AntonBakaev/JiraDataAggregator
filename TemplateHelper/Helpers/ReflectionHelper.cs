using System;
using System.Collections.Generic;
using System.Reflection;
using TemplateHelper.Common;

namespace TemplateHelper.Helpers
{
	internal class ReflectionHelper
	{
		public static List<PropertyData> GetPropertyData(Type type, object data)
		{
			var typeInfoList = new List<PropertyData>();

			PropertyInfo[] propertiesInfoList = type.GetProperties();

			foreach (var property in propertiesInfoList)
			{
				Type modelType;
				bool isList;

				Type propType = property.PropertyType;
				if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(List<>))
				{
					Type innerType = propType.GetGenericArguments()[0];
					modelType = innerType;
					isList = true;
				}
				else
				{
					modelType = property.PropertyType;
					isList = false;
				}

				object value = data.GetType().GetProperty(property.Name).GetValue(data, null);

				typeInfoList.Add(new PropertyData(modelType, isList, property.Name, value));
			}

			return typeInfoList;
		}
	}
}
