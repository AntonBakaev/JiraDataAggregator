using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectVm
	{
		[XmlAttribute("key")]
		public string DefectName { get; set; }

		[XmlAttribute("link")]
		public string Link { get; set; }

		public List<IssueVm> BlockingIssues { get; set; }

		[XmlIgnore]
		public int BlockingIssuesCount { get { return BlockingIssues.Count; } }
	}
}
