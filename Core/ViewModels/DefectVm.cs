﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectVm
	{
		[XmlElement("key")]
		public string DefectName { get; set; }

		[XmlElement("link")]
		public string Link { get; set; }

		[XmlArray("blockedIssues")]
		[XmlArrayItem("issue", typeof(IssueVm))]
		public List<IssueVm> BlockingIssues { get; set; }

		[XmlIgnore]
		public int BlockingIssuesCount { get { return BlockingIssues.Count; } }
	}
}
