using Core.Aggregators.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Aggregators
{
	public class DefectReportAggregator : IDefectReportAggregator
	{
        private readonly IDefectReportRepository defectRepository;

	    public DefectReportAggregator(IDefectReportRepository repository)
	    {
	        this.defectRepository = repository;
	    }
    }
}
