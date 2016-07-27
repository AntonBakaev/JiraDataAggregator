﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Core.ViewModels
{
	[Serializable]
	public class AllDefectKeysVm
	{
		[XmlArray("defectKeys")]
		[XmlArrayItem("defectKey")]
		public List<string> AllDefectKeys { get; set; }
	}
}
