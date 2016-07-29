using System;

namespace TemplateHelper.Common
{
	internal class PropertyData
	{
		public PropertyData(Type modelType, bool isList, string propertyName, object value)
		{
			ModelType = modelType;
			IsList = isList;
			PropertyName = propertyName;
			Value = value;
		}

		public Type ModelType { get; private set; }
		public bool IsList { get; private set; }
		public string PropertyName { get; private set; }
		public object Value { get; private set; }

		public bool IsModelTypeSimple
		{
			get { return ModelType.IsPrimitive || ModelType == typeof(string); }
		}
	}
}
