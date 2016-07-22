using System.Collections.Generic;
using Common.Enums;

namespace Core.Models
{
	public class Execution
	{
		public string ExecutionId { get; set; }
		public string CycleName { get; set; }
		public string IssueKey { get; set; }
		public string TestSummary { get; set; }
		public string Project { get; set; }
		public string Versions { get; set; }
		public ComponentType Components { get; set; }
		public PriorityType Priority { get; set; }
		public string ExecutedBy { get; set; }
		public string ExecutedOn { get; set; }
		public ExecutedStatus ExecutedStatus { get; set; }
		public string ExecutionDefects { get; set; }
		public string CreationDate { get; set; }
		public List<TestStep> TestSteps { get; set; } 
	}
}
