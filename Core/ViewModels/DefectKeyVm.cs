using System;
using System.Xml.Serialization;
using Core.ViewModels.Interfaces;

namespace Core.ViewModels
{
	[Serializable]
	public class DefectKeyVm : IViewModel
	{
		[XmlAttribute("value")]
		public string Value { get; set; }
	}
}
