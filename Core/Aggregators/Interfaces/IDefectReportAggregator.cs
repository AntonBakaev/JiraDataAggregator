using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Aggregators.Interfaces
{
	public interface IDefectReportAggregator
	{
		IEnumerable<Execution> GetExecutions(string fileName);
		Task<Dictionary<string, DefectInfo>> GetExecutionsDefectInfo(IEnumerable<Execution> executions);
		IEnumerable<Execution> Filter(IEnumerable<Execution> executions, Dictionary<string, DefectInfo> defectInfoList);
	}
}
