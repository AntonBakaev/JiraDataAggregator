using Core.Aggregators;
using Core.Aggregators.Interfaces;
using Core.Repositories;
using Core.Repositories.Interfaces;
using StructureMap;

namespace JiraDataAggregator
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression x)
        {
            x.For<IDefectReportAggregator>().Use<DefectReportAggregator>();
            x.For<IDefectReportRepository>().Use<DefectReportRepository>();
        }
    }
}
