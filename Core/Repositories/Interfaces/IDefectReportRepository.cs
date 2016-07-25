using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories.Interfaces
{
	public interface IDefectReportRepository
	{
		IEnumerable<Execution> GetDeserilizationData(string filePath);
	}
}
