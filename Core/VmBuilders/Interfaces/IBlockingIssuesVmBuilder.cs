using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;

namespace Core.VmBuilders.Interfaces
{
	public interface IBlockingIssuesVmBuilder
	{
		BlockingIssuesVm GetTopBlockingIssues(IEnumerable<Execution> executionsList, int numberOfBlockingIssues);
	}
}
