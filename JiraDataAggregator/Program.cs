using System.Collections.Generic;
using Common.Helpers;
using Core.Aggregators.Interfaces;
using Core.Models;
using IoC.Initialize;

namespace JiraDataAggregator
{
	public class Program
	{
		public class ConsoleRunner
		{
			private readonly IDefectReportAggregator defectAggregator;

			public ConsoleRunner(IDefectReportAggregator manager)
			{
				this.defectAggregator = manager;
			}

			public void Execute()
			{
			}
		}

		static void Main(string[] args)
		{
			Application.Initialize(ConfigurationHelper.ConfigureDependencies);
			Application.Current.Container.GetInstance<ConsoleRunner>().Execute();

			List<Execution> executionsList = SerializeHelper.DeserializeXml(args[0]);
		}
	}
}
