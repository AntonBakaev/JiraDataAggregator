namespace TemplateHelper.Helpers
{
	internal class DefaultOutputFormatter : IOutputFormatter
	{
		public string ToOutPutString(string key, string value)
		{
			return value;
		}
	}
}
