namespace TemplateHelper.Helpers
{
	internal class DefaultInputFormatter : IInputFormatter
	{
		public string InputSearchPattern { get { return @"\{(.*?)\}"; } }
		public string InputKeyPattern { get { return "{{{0}}}"; } }
	}
}
