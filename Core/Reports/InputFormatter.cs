using TemplateHelper;

namespace Core.Reports
{
	public class InputFormatter : IInputFormatter
	{
		public string InputSearchPattern
		{
			get { return @"\[(.*?)\]"; }
		}

		public string InputKeyPattern
		{
			get { return @"\[{0}\]"; }
		}
	}
}
