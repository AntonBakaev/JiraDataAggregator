using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class BlockingIssuesVm : IViewModel
	{
		[XmlArray("blockingDefects")]
		[XmlArrayItem("defect", typeof(DefectVm))]
		public List<DefectVm> DefectsList { get; set; }
	}
}
