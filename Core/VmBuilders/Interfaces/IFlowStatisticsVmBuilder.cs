using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;

namespace Core.VmBuilders.Interfaces
{
	public interface IFlowStatisticsVmBuilder
	{
		FlowStatisticsVm GetFlowStatisticsVm(List<Execution> executionsList);
		FlowStatisticsVm GetFlowStatisticsVmByFilter(List<Execution> executionsList);
	}
}
