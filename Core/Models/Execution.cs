using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Core.Enums;

namespace Core.Models
{
	[Serializable]
	[XmlType("execution")]
	public class Execution
	{
		private const string DefectsCategoriesSeparator = " | ";
		private const char DefectsSeparator = ',';
		private IEnumerable<string> executionDefects;

		[XmlElement("executionId")]
		public string ExecutionId { get; set; }

		[XmlElement("cycleName")]
		public string CycleName { get; set; }

		[XmlElement("issueKey")]
		public string IssueKey { get; set; }

		[XmlElement("testSummary")]
		public string TestSummary { get; set; }

		[XmlElement("project")]
		public string Project { get; set; }

		[XmlElement("versions")]
		public string Versions { get; set; }

		[XmlElement("components", typeof(ComponentType))]
		public ComponentType Components { get; set; }

		[XmlElement("priority", typeof(PriorityType))]
		public PriorityType Priority { get; set; }

		[XmlElement("executedBy")]
		public string ExecutedBy { get; set; }

		[XmlElement("executedOn")]
		public string ExecutedOn { get; set; }

		[XmlElement("executedStatus", typeof(ExecutedStatus))]
		public ExecutedStatus ExecutedStatus { get; set; }

		[XmlElement("executionDefects")]
		public string AllExecutionDefectsFullString { get; set; }

		[XmlElement("creationDate")]
		public string CreationDate { get; set; }

		[XmlArray("teststeps")]
		[XmlArrayItem("teststep", typeof(TestStep))]
		public List<TestStep> TestSteps { get; set; }

		[XmlIgnore]
		public IEnumerable<string> ExecutionDefects
		{
			get
			{
				return executionDefects ?? (executionDefects = AllExecutionDefectsFullString
																  .Replace(DefectsCategoriesSeparator, DefectsSeparator.ToString())
																  .Split(DefectsSeparator)
																  .Distinct());
			}
		}
	}
}
