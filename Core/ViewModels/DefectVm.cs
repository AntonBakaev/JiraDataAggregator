using System.Collections.Generic;

namespace Core.ViewModels
{
	public class DefectVm
	{
		public string DefectName { get; set; }
		public string Link { get; set; }
		public List<IssueVm> BlockingIssues { get; set; }
	}
}
