using IoC.Initialize;
using System;
using System.Collections.Generic;
using Core.Managers.Interfaces;

namespace JiraDataAggregator
{
	public class Program
	{
        public class ConsoleRunner
        {
            private readonly IDefectReportManager defectManager;

            public ConsoleRunner(IDefectReportManager manager)
            {
                this.defectManager = manager;
            }
            
            public void Execute()
            {
                Console.WriteLine(defectManager.GetMessage());
            }
        }

		static void Main(string[] args)
		{
            Application.Initialize(ConfigurationHelper.ConfigureDependencies);
            Application.Current.Container.GetInstance<ConsoleRunner>().Execute();
		}
	}
}
