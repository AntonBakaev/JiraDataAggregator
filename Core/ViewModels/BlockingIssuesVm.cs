using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class BlockingIssuesVm
	{
		[XmlArray("blockingDefects")]
		[XmlArrayItem("defect", typeof(DefectVm))]
		public List<DefectVm> DefectsList { get; set; }
	}
}
