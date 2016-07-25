using System;
using System.Collections.Generic;
using System.Configuration;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;
using IoC.Initialize;

namespace JiraDataAggregator
{
	public class Program
	{
		public class ConsoleRunner
		{
			private readonly IDefectReportAggregator defectReportAggregator;
			private readonly IAllBlockingDefectsVmBuilder allBlockingDefectsVmBuilder;
			private readonly IBlockingIssuesVmBuilder blockingIssuesVmBuilder;
			private readonly IFlowStatisticsVmBuilder flowStatisticsVmBuilder;

			public ConsoleRunner(IDefectReportAggregator defectReportAggregator,
								 IAllBlockingDefectsVmBuilder allBlockingDefectsVmBuilder,
								 IBlockingIssuesVmBuilder blockingIssuesVmBuilder,
								 IFlowStatisticsVmBuilder flowStatisticsVmBuilder)
			{
				this.defectReportAggregator = defectReportAggregator;
				this.allBlockingDefectsVmBuilder = allBlockingDefectsVmBuilder;
				this.blockingIssuesVmBuilder = blockingIssuesVmBuilder;
				this.flowStatisticsVmBuilder = flowStatisticsVmBuilder;
			}

			public void Execute(string fileName)
			{
				IEnumerable<Execution> executionsList = defectReportAggregator.GetDeserializedExecutions(fileName);

				FlowStatisticsVm flowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVm(executionsList);
				FlowStatisticsVm filteredFlowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVmByFilter(executionsList);

				IEnumerable<DefectVm> blockingIssuesList = blockingIssuesVmBuilder.GetTopBlockingIssues(executionsList,
																 Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfTopBlockingIssues"]));

				IEnumerable<string> allBlockingDefects = allBlockingDefectsVmBuilder.GetAllBlockingDefects(executionsList);
			}
		}

		static void Main(string[] args)
		{
			Application.Initialize(ConfigurationHelper.ConfigureDependencies);
			Application.Current.Container.GetInstance<ConsoleRunner>().Execute(args[0]);
		}
	}
}
