using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Enums;
using Core.Models;

namespace Core.Repositories.Interfaces
{
	public interface IDefectReportRepository
	{
		IEnumerable<Execution> GetIsitLaunchCriticalViewData(string filePath);

		Task<IssueStatus> GetIssueStatus(string issueKey);
	}
}
