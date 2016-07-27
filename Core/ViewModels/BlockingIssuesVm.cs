using System;
using System.Collections.Generic;

namespace Core.ViewModels
{
	[Serializable]
	public class BlockingIssuesVm
	{
		public IEnumerable<DefectVm> DefectsList { get; set; }
	}
}
