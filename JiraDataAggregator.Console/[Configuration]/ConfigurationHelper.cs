﻿using System;
using Common.Helpers;
using Common.Helpers.Interfaces;
using Core.Aggregators;
using Core.Aggregators.Interfaces;
using Core.Repositories;
using Core.Repositories.Interfaces;
using Core.VmBuilders;
using Core.VmBuilders.Interfaces;
using JiraDataAggregator.Console.Utilities;
using StructureMap;

namespace JiraDataAggregator._Configuration_
{
	public class ConfigurationHelper
	{
		public static void ConfigureDependencies(ConfigurationExpression x)
		{
			x.For<IDefectReportAggregator>().Use<DefectReportAggregator>();
			x.For<IDefectReportRepository>().Use<DefectReportRepository>();

			x.For<IAllDefectKeysVmBuilder>().Use<AllDefectKeysVmBuilder>();
			x.For<IBlockingIssuesVmBuilder>().Use<BlockingIssuesVmBuilder>();
			x.For<IFlowStatisticsVmBuilder>().Use<FlowStatisticsVmBuilder>();
			x.For<ILogger>().Use<ConsoleLogger>();
		}
	}
}