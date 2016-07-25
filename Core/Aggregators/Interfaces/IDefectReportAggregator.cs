using System.Collections.Generic;
using Core.Models;

namespace Core.Aggregators.Interfaces
{
	public interface IDefectReportAggregator
	{
		IEnumerable<Execution> GetDeserializedExecutions(string fileName);
	}
}
