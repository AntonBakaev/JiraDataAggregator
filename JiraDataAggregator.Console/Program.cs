using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Common.Helpers.Interfaces;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.Reports;
using Core.Reports.Interfaces;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;
using IoC.Initialize;
using JiraDataAggregator.Console;

namespace JiraDataAggregator.Console
{
	public class Program
	{
		public class ConsoleRunner
		{
			private readonly IDefectReportAggregator defectReportAggregator;
			private readonly IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder;
			private readonly IBlockingIssuesVmBuilder blockingIssuesVmBuilder;
			private readonly IFlowStatisticsVmBuilder flowStatisticsVmBuilder;
			private readonly IRtfDefectReporter rtfDefectReporter;
			private readonly IXmlDefectReporter xmlDefectReporter;

			public ConsoleRunner(IDefectReportAggregator defectReportAggregator,
								 IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder,
								 IBlockingIssuesVmBuilder blockingIssuesVmBuilder,
								 IFlowStatisticsVmBuilder flowStatisticsVmBuilder,
								 IRtfDefectReporter rtfDefectReporter,
								 IXmlDefectReporter xmlDefectReporter)
			{
				this.defectReportAggregator = defectReportAggregator;
				this.allBlockingDefectsVmBuilder = allBlockingDefectsVmBuilder;
				this.blockingIssuesVmBuilder = blockingIssuesVmBuilder;
				this.flowStatisticsVmBuilder = flowStatisticsVmBuilder;
				this.rtfDefectReporter = rtfDefectReporter;
				this.xmlDefectReporter = xmlDefectReporter;
			}

			public async Task Execute(string fileName)
			{
				IEnumerable<Execution> executionsList = await defectReportAggregator.GetIsitLaunchCriticalViewData(fileName);

				DefectReportVm defectReportVm = GenerateDefectReportVm(executionsList);

				xmlDefectReporter.Generate(defectReportVm);
				rtfDefectReporter.Generate(defectReportVm);
			}

			private DefectReportVm GenerateDefectReportVm(IEnumerable<Execution> executionsList)
			{
				FlowStatisticsVm flowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVm(executionsList);
				FlowStatisticsVm filteredFlowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVmByFilter(executionsList);

				BlockingIssuesVm blockingIssuesList = blockingIssuesVmBuilder.GetTopBlockingIssues(executionsList,
																 Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfTopBlockingIssues"]));

				AllDefectKeysVm allDefectKeys = allBlockingDefectsVmBuilder.GetAllBlockingDefects(executionsList);

				return new DefectReportVm()
				{
					FlowStatisticsVm = flowStatistics,
					RetailShopFlowStatisticsVm = filteredFlowStatistics,
					BlockingIssuesVm = blockingIssuesList,
					AllDefectKeysVm = allDefectKeys
				};
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Application.Initialize(ConfigurationHelper.ConfigureDependencies);
				Application.Current.Container.GetInstance<ConsoleRunner>().Execute(args[0]).Wait();
			}
			catch
			{
				System.Console.WriteLine("The program stopped working due to internal errors.");
			}
		}
	}
}
