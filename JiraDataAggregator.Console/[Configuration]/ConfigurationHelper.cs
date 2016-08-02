using System.Collections.Generic;
using Common.Helpers;
using Common.Helpers.Interfaces;
using Common.Utilities;
using Common.Utilities.Interfaces;
using Core.Aggregators;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.Reports;
using Core.Reports.Interfaces;
using Core.Repositories;
using Core.Repositories.Interfaces;
using Core.ViewModels;
using Core.VmBuilders;
using Core.VmBuilders.Interfaces;
using DataAccess.RestServices;
using DataAccess.RestServices.Interfaces;
using NLog;
using StructureMap;

namespace JiraDataAggregator.Console
{
	public class ConfigurationHelper
	{
		public static void ConfigureDependencies(ConfigurationExpression x)
		{
			x.For<IDefectReportAggregator>().Use<DefectReportAggregator>();
			x.For<IDefectReportRepository>().Use<DefectReportRepository>();

			x.For<IFlowStatisticsVmBuilder>().Use<FlowStatisticsVmBuilder>();
			x.For<IBlockingIssuesVmBuilder>().Use<BlockingIssuesVmBuilder>();
			x.For<IAllDefectKeysVmBuilder>().Use<AllDefectKeysVmBuilder>();

			x.For<IRtfDefectReporter>().Use<RtfDefectReporter>();
			//x.For<IRtfDefectReporter>().Use<AutomaticRtfDefectReporter>();
			x.For<IXmlDefectReporter>().Use<XmlDefectReporter>();

			x.For<ISerializeHelper<List<Execution>>>().Use<SerializeHelper<List<Execution>>>();
			x.For<ISerializeHelper<DefectReportVm>>().Use<SerializeHelper<DefectReportVm>>();

			x.For<ILoggerFactory>().Singleton().Use<LoggerFactory>();
			x.For<ILogger>().AlwaysUnique().Use(context => context.GetInstance<ILoggerFactory>().Create(context.ParentType ?? context.RootType));
			x.For<IRestClient>().Use<RestClient>();
		}
	}
}
