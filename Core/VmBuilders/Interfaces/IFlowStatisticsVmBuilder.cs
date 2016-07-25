using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;

namespace Core.VmBuilders.Interfaces
{
	public interface IFlowStatisticsVmBuilder
	{
		FlowStatisticsVm GetFlowStatisticsVm(IEnumerable<Execution> executionsList);
		FlowStatisticsVm GetFlowStatisticsVmByFilter(IEnumerable<Execution> executionsList);
	}
}
