using System.Collections.Generic;
using Common.Helpers;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
		public IEnumerable<Execution> GetDeserilizationData(string filePath)
		{
			return SerializeHelper<Execution>.DeserializeXml(filePath);
		}
	}
}
