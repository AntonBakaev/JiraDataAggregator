using Core.Managers.Interfaces;
using IoC.Initialize;

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

            }
        }

		static void Main(string[] args)
		{
            Application.Initialize(ConfigurationHelper.ConfigureDependencies);
            Application.Current.Container.GetInstance<ConsoleRunner>().Execute();
		}
	}
}
