using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Aggregators.Interfaces
{
	public interface IDefectReportAggregator
	{
		Task<IEnumerable<Execution>> GetIsitLaunchCriticalViewData(string fileName);
	}
}
