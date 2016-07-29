using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class AllDefectKeysVm : IViewModel
	{
		[XmlArray("defectKeys")]
		[XmlArrayItem("defectKey")]
		public List<DefectKeyVm> AllDefectKeys { get; set; }
	}
}
