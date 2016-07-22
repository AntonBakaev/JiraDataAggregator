using Core.Managers;
using Core.Managers.Interfaces;
using Core.Repositories;
using Core.Repositories.Interfaces;
using StructureMap;

namespace JiraDataAggregator
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression x)
        {
            x.For<IDefectReportManager>().Use<DefectReportManager>();
            x.For<IDefectReportRepository>().Use<DefectReportRepository>();
        }
    }
}
