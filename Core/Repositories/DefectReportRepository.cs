using System.Collections.Generic;
using Core.Models;
using Core.Repositories.Interfaces;
using Common.Helpers;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
		public IEnumerable<Execution> GetIsitLaunchCriticalViewData(string filePath)
		{
			return SerializeHelper<Execution>.DeserializeXml(filePath);
		}
	}
}
