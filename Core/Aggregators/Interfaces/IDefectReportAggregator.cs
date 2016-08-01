using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Aggregators.Interfaces
{
	public interface IDefectReportAggregator
	{
		IEnumerable<Execution> GetExecutions(string fileName);
		Task<IEnumerable<Execution>> FilterExecutions(IEnumerable<Execution> executions);
	}
}
