using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;


namespace Common.Helpers
{
	public class ConvertHelper
	{
		public static Dictionary<string, string> ToDictionary(object data)
		{
			if (data == null)
				return null;

			return data.GetType().GetProperties()
				.ToDictionary(x => x.Name, x => x.GetGetMethod().Invoke(data, null) == null
					? ""
					: x.GetGetMethod().Invoke(data, null).ToString());
		}

		public static string ToEnumString<T>(T type)
		{
			var enumType = typeof(T);
			var name = Enum.GetName(enumType, type);
			var enumMemberAttribute = ((EnumMemberAttribute[])enumType
				.GetField(name)
				.GetCustomAttributes(typeof(EnumMemberAttribute), true))
				.Single();

			return enumMemberAttribute.Value;
		}

		public static T ToEnum<T>(string str)
		{
			var enumType = typeof(T);
			foreach (var name in Enum.GetNames(enumType))
			{
				var enumMemberAttribute = ((EnumMemberAttribute[])enumType
					.GetField(name)
					.GetCustomAttributes(typeof(EnumMemberAttribute), true))
					.FirstOrDefault();

				if (enumMemberAttribute != null && enumMemberAttribute.Value == str)
				{
					return (T)Enum.Parse(enumType, name);
				}
			}

			return default(T);
		}
	}
}
