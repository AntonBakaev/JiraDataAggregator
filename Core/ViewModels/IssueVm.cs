using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class IssueVm
	{
		public string IssueName { get; set; }
		public List<string> TestCases { get; set; } 
	}
}
