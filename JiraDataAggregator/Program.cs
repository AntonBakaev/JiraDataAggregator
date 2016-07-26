using System;
using System.Collections.Generic;
using System.Configuration;
using Common.Helpers;
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
			private readonly IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder;
			private readonly IBlockingIssuesVmBuilder blockingIssuesVmBuilder;
			private readonly IFlowStatisticsVmBuilder flowStatisticsVmBuilder;

			public ConsoleRunner(IDefectReportAggregator defectReportAggregator,
								 IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder,
								 IBlockingIssuesVmBuilder blockingIssuesVmBuilder,
								 IFlowStatisticsVmBuilder flowStatisticsVmBuilder)
			{
				this.defectReportAggregator = defectReportAggregator;
				this.allBlockingDefectsVmBuilder = allBlockingDefectsVmBuilder;
				this.blockingIssuesVmBuilder = blockingIssuesVmBuilder;
				this.flowStatisticsVmBuilder = flowStatisticsVmBuilder;
			}

			public async void Execute(string fileName)
			{
				IEnumerable<Execution> executionsList = await defectReportAggregator.GetIsitLaunchCriticalViewData(fileName);

				FlowStatisticsVm flowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVm(executionsList);
				FlowStatisticsVm filteredFlowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVmByFilter(executionsList);

				BlockingIssuesVm blockingIssuesList = blockingIssuesVmBuilder.GetTopBlockingIssues(executionsList,
																 Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfTopBlockingIssues"]));

				AllDefectKeysVm allDefectKeys = allBlockingDefectsVmBuilder.GetAllBlockingDefects(executionsList);

                Dictionary<string, string> fs = ConvertHelper.ToDictionary(flowStatistics);
                Dictionary<string, string> ffs = ConvertHelper.ToDictionary(filteredFlowStatistics);
                Dictionary<string, string> bil = ConvertHelper.ToDictionary(blockingIssuesList);
                Dictionary<string, string> adk = ConvertHelper.ToDictionary(allDefectKeys);

                Dictionary<string, string> allModels = new Dictionary<string, string>();

                foreach (var key in fs.Keys)
			    {
			        allModels.Add(key, fs[key]);
			    }
                foreach (var key in ffs.Keys)
                {
                    allModels.Add(key, ffs[key]);
                }
                foreach (var key in bil.Keys)
                {
                    allModels.Add(key, bil[key]);
                }
                foreach (var key in adk.Keys)
                {
                    allModels.Add(key, adk[key]);
                }

                ReportGeneratorHelper.GenerateReport(allModels);
			}
		}

		static void Main(string[] args)
		{
			Application.Initialize(ConfigurationHelper.ConfigureDependencies);
			Application.Current.Container.GetInstance<ConsoleRunner>().Execute(args[0]);
		}
	}
}
