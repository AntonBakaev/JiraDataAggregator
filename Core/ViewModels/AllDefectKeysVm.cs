using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class AllDefectKeysVm
	{
		[XmlArrayItem("defectKey")]
		public IEnumerable<string> AllDefectKeys { get; set; }
	}
}
