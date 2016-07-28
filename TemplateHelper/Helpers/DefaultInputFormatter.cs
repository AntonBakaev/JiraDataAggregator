namespace TemplateHelper.Helpers
{
	internal class DefaultInputFormatter : IInputFormatter
	{
		public string InputPattern { get { return @"\{(.*?)\}"; } }
		public string InputKeyPattern { get { return "{{{0}}}"; } }
	}
}
