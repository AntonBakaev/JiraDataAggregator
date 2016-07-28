using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Common.Helpers.Interfaces;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.Reports;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;
using IoC.Initialize;
using JiraDataAggregator._Configuration_;

namespace JiraDataAggregator.Console
{
	public class Program
	{
		public class ConsoleRunner
		{
			private readonly IDefectReportAggregator defectReportAggregator;
			private readonly IDateTimeVmBuilder dateTimeVmBuilder;
			private readonly IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder;
			private readonly IBlockingIssuesVmBuilder blockingIssuesVmBuilder;
			private readonly IFlowStatisticsVmBuilder flowStatisticsVmBuilder;
			private readonly ISerializeHelper<DefectReportVm> serializeHelper;

			public ConsoleRunner(IDefectReportAggregator defectReportAggregator,
								 IDateTimeVmBuilder dateTimeVmBuilder,
								 IAllDefectKeysVmBuilder allBlockingDefectsVmBuilder,
								 IBlockingIssuesVmBuilder blockingIssuesVmBuilder,
								 IFlowStatisticsVmBuilder flowStatisticsVmBuilder,
								 ISerializeHelper<DefectReportVm> serializeHelper)
			{
				this.defectReportAggregator = defectReportAggregator;
				this.allBlockingDefectsVmBuilder = allBlockingDefectsVmBuilder;
				this.blockingIssuesVmBuilder = blockingIssuesVmBuilder;
				this.flowStatisticsVmBuilder = flowStatisticsVmBuilder;
				this.serializeHelper = serializeHelper;
				this.dateTimeVmBuilder = dateTimeVmBuilder;
			}

			public async Task Execute(string fileName)
			{
				IEnumerable<Execution> executionsList = await defectReportAggregator.GetIsitLaunchCriticalViewData(fileName);

				DefectReportVm defectReportVm = GenerateDefectReportVm(executionsList);

				GenerateXmlDefectReport(defectReportVm);
				GenerateRtfDefectReport(defectReportVm);
			}

			private DefectReportVm GenerateDefectReportVm(IEnumerable<Execution> executionsList)
			{
				FlowStatisticsVm flowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVm(executionsList);
				FlowStatisticsVm filteredFlowStatistics = flowStatisticsVmBuilder.GetFlowStatisticsVmByFilter(executionsList);

				BlockingIssuesVm blockingIssuesList = blockingIssuesVmBuilder.GetTopBlockingIssues(executionsList,
																 Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfTopBlockingIssues"]));

				AllDefectKeysVm allDefectKeys = allBlockingDefectsVmBuilder.GetAllBlockingDefects(executionsList);
				DateTimeVm dateTimeVm = dateTimeVmBuilder.GetDateTimeWhenReportGenerated();

				return new DefectReportVm()
				{
					DateTimeVm = dateTimeVm,
					FlowStatisticsVm = flowStatistics,
					RetailShopFlowStatisticsVm = filteredFlowStatistics,
					BlockingIssuesVm = blockingIssuesList,
					AllDefectKeysVm = allDefectKeys
				};
			}

			private void GenerateXmlDefectReport(DefectReportVm defectReportVm)
			{
				var xmlReporter = new XmlDefectReporter(serializeHelper);
				xmlReporter.Generate(defectReportVm);
			}

			private void GenerateRtfDefectReport(DefectReportVm defectReportVm)
			{
				var rtfReporter = new RtfDefectReporter();
				rtfReporter.Generate(defectReportVm);
			}
		}

		static void Main(string[] args)
		{
			Application.Initialize(ConfigurationHelper.ConfigureDependencies);
			Application.Current.Container.GetInstance<ConsoleRunner>().Execute(args[0]).Wait();
		}
	}
}
