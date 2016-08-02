using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;
using System.Configuration;

namespace Core.VmBuilders
{
	public class FlowStatisticsVmBuilder : IFlowStatisticsVmBuilder
	{
		private const string FilterFlowStatistics = "FilterFlowStatistics";

		public FlowStatisticsVm GetFlowStatisticsVm(IEnumerable<Execution> executionsList)
		{
			return new FlowStatisticsVm
			{
				Passed = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Pass),
				Blocked = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Blocked),
				Failed = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.Fail),
				Wip = executionsList.Count(e => e.ExecutedStatus == ExecutedStatus.WIP),
			};
		}

		public FlowStatisticsVm GetFlowStatisticsVmByFilter(IEnumerable<Execution> executionsList)
		{
			return GetFlowStatisticsVm(executionsList.Where(e => e.TestSummary.
				Trim().
				StartsWith(ConfigurationManager.
				AppSettings[FilterFlowStatistics])).ToList());
		}
	}
}
