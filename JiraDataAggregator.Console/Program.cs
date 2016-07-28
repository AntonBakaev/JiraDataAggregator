using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers.Interfaces;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.Reports;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;
using IoC.Initialize;
using JiraDataAggregator._Configuration_;
using TemplateHelper;

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
			private readonly ISerializeHelper<DefectReportVm> serializeHelper;

			public ConsoleRunner(IDefectReportAggregator defectReportAggregator,
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
			}

			public async Task Execute(string fileName)
			{
				string basePath = @"C:\Teaakov\JiraDataAggregator\JiraDataAggregator.Console\bin\Debug\Templates\{0}Template.rtf";

				var allTemplates = new Dictionary<Type, string>
			{
				{typeof (IssueVm), "IssueVm"},
				{typeof (AllDefectKeysVm), "AllDefectKeysVm"},
				{typeof (BlockingIssuesVm), "BlockingIssuesVm"},
				{typeof (DefectKeyVm), "DefectKeyVm"},
				{typeof (DefectVm), "DefectVm"},
				{typeof (FlowStatisticsVm), "FlowStatisticsVm"},
				{typeof (DefectReportVm), "DefectReportVm"},

			};

				var keys = new List<Type>(allTemplates.Keys);

				foreach (Type key in keys)
				{
					string path = String.Format(basePath, allTemplates[key]);
					string text;
					using (var streamReader = new StreamReader(path, Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
					allTemplates[key] = text;
				}

				IEnumerable<Execution> executionsList = await defectReportAggregator.GetIsitLaunchCriticalViewData(fileName);

				DefectReportVm defectReportVm = GenerateDefectReportVm(executionsList);
				var inputFormatter = new InputFormatter();
				var replacer = new TemplateReplacer(inputFormatter);

				string actualResult = replacer.Replace(defectReportVm, allTemplates);
				using (var streamWriter = new StreamWriter("result.rtf"))
				{
					streamWriter.Write(actualResult);
				}

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

				return new DefectReportVm()
				{
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
			Application.Current.Container.GetInstance<ConsoleRunner>().Execute(@"C:\Teaakov\example1.xml").Wait();
		}
	}

	public class InputFormatter : IInputFormatter
	{
		public string InputSearchPattern
		{
			get { return @"\[(.*?)\]}"; }
		}

		public string InputKeyPattern
		{
			get { return @"\[{0}\]"; }
		}
	}
}
