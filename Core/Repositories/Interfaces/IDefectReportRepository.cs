using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories.Interfaces
{
	public interface IDefectReportRepository
	{
		IEnumerable<Execution> GetIsitLaunchCriticalViewData(string filePath);
	}
}
