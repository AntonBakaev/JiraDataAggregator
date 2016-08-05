using Core.Enums;

namespace Core.Models
{
	public class DefectInfo
	{
		public string Status { set; get; }

		public string Assignee { get; set; }

		public string Components { get; set; }

		public string Summary { get; set; }

		public string Severity { get; set; }
	}
}
