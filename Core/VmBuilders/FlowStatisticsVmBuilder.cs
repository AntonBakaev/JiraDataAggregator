using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class FlowStatisticsVmBuilder : IFlowStatisticsVmBuilder
	{
		private const string RetailStr = "Shop_E2E: Retail";

		public FlowStatisticsVm GetFlowStatisticsVm(IEnumerable<Execution> executionsList)
		{
			return new FlowStatisticsVm
			{
				Passed = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Pass),
				Blocked = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Blocked),
				Failed = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Fail),
				Wip = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.WIP),
				IsFiltered = false
			};
		}

		public FlowStatisticsVm GetFlowStatisticsVmByFilter(IEnumerable<Execution> executionsList)
		{
			var newExecutionsList = executionsList.Where(e => e.TestSummary.Trim().StartsWith(RetailStr)).ToList();

			return new FlowStatisticsVm
			{
				Passed = newExecutionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Pass),
				Blocked = newExecutionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Blocked),
				Failed = newExecutionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Fail),
				Wip = newExecutionsList.Count(e => e.ExecutedStatus == ExecutedStatus.WIP),
				Filter = RetailStr,
				IsFiltered = true
			};
		}
	}
}
