using System.Collections.Generic;
using Core.Models;

namespace Core.Aggregators.Interfaces
{
	public interface IDefectReportAggregator
	{
		IEnumerable<Execution> GetIsitLaunchCriticalViewData(string fileName);
	}
}
