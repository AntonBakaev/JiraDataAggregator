using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TemplateReplacer;
using TemplateReplacer.Helpers;

namespace TemplateHelper
{
	public class TemplateReplacer : ITemplateReplacer
	{
		private const string PropertyNameRegexPattern = @"\{(.*?)\}";

		public string Replace(IReplaceable data, Dictionary<Type, string> allTemplates)
		{
			Type type = data.GetType();

			if (!allTemplates.ContainsKey(type))
				throw new ArgumentException(String.Format("Template for {0} was not found", type.Name));

			string template = allTemplates[type];
			var propDataList = ReflectionHelper.GetPropertyData(type, data);
			var replaceDict = new Dictionary<string, string>();
			var matches = Regex.Matches(template, PropertyNameRegexPattern);

			foreach (Match match in matches)
			{
				var text = match.Groups[1].Value;
				var propData = PropertyDataHelper.GetPropertyByName(propDataList, text);
				if (propData == null) 
					continue;

				string key = String.Format("{{{0}}}", text);
				string value = "";

				if (propData.IsModelTypeSimple)
				{
					value = propData.Value.ToString();
				}
				else
				{
					if (!propData.IsList)
					{
						value = Replace((IReplaceable)propData.Value, allTemplates);
					}
					else
					{
						var dataCollection = (IEnumerable)propData.Value;
						value = dataCollection.Cast<IReplaceable>()
							.Aggregate(value, (current, dataItem) => String.Concat(current, Replace(dataItem, allTemplates)));
					}
				}

				if (!replaceDict.ContainsKey(key))
					replaceDict.Add(key, value);
			}

			return replaceDict.Aggregate(template, (current, item) => Regex.Replace(current, item.Key, item.Value));
		}
	}
}
