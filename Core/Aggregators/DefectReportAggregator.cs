using System.Collections.Generic;
using Core.Aggregators.Interfaces;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Core.Aggregators
{
	public class DefectReportAggregator : IDefectReportAggregator
	{
        private readonly IDefectReportRepository defectReportRepository;

		public DefectReportAggregator(IDefectReportRepository defectReportRepository)
	    {
			this.defectReportRepository = defectReportRepository;
	    }

		public IEnumerable<Execution> GetDeserializedExecutions(string fileName)
		{
			return defectReportRepository.GetDeserilizationData(fileName);
		}
	}
}
