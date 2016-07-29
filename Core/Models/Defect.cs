using System.Collections.Generic;

namespace Core.Models
{
	public class Defect
	{
		public string Key { get; set; }
		public List<string> BlockingIssuesKeys { get; set; }
	}
}
