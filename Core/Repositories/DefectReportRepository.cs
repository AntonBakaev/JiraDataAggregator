using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.Interfaces;
using Common.Helpers;
using Core.Enums;
using DataAccess.RestServices;
using Newtonsoft.Json.Linq;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
		private RestClient restClient;

		public DefectReportRepository(RestClient restClient)
		{
			this.restClient = restClient;
		}

		public IEnumerable<Execution> GetIsitLaunchCriticalViewData(string filePath)
		{
			return SerializeHelper<Execution>.DeserializeXml(filePath);
		}

		public async Task<IssueStatus> GetIssueStatus(string issueKey)
		{
			var dataObject = await restClient.Get<object>("GetIssueStatus", new { issueKey }); 
			//var dataObject = await restClient.Get<object>("GetIssueStatus", new string[] { issueKey }, new { fields = "status" });



			string statusString = JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString();

			return ConvertHelper.ToEnum<IssueStatus>(statusString);
		}
	}
}