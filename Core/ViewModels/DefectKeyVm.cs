using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectKeyVm
	{
		[XmlAttribute("defectKey")]
		public string DefectKey { get; set; }
	}
}
