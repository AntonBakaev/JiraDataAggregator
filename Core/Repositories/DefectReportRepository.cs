using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Helpers.Interfaces;
using Core.Enums;
using Core.Models;
using Core.Repositories.Interfaces;
using DataAccess.RestServices.Interfaces;
using Newtonsoft.Json.Linq;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
		private readonly IRestClient restClient;
		private readonly ISerializeHelper<List<Execution>> serializeHelper;

		public DefectReportRepository(IRestClient restClient, ISerializeHelper<List<Execution>> serializeHelper)
		{
			this.restClient = restClient;
			this.serializeHelper = serializeHelper;
		}

		public IEnumerable<Execution> GetIsitLaunchCriticalViewData(string filePath)
		{
			return serializeHelper.DeserializeXml(filePath);
		}

		public async Task<IssueStatus> GetIssueStatus(string issueKey)
		{
			var dataObject = await restClient.Get<object>("GetIssueStatus", new { issueKey });

			string statusString = JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString();

			return ConvertHelper.ToEnum<IssueStatus>(statusString);
		}

		public async Task<DefectInfo> GetIssueInfo(string issueKey)
		{
			string statusString = String.Empty;
			string assigneeName = String.Empty;
			string componentsName = String.Empty;
			string severityValue = String.Empty;
			string summaryValue = String.Empty;

			var dataObject = await restClient.Get<object>("GetIssueInfo", new { issueKey });
			JObject jObject = JObject.FromObject(dataObject);
			var status = jObject["fields"]["status"];
			if (status != null && status.HasValues)
				statusString = status["name"].ToString();
			var assignee = jObject["fields"]["assignee"];
			if (assignee != null && assignee.HasValues)
				assigneeName = assignee["name"].ToString();
			var components = jObject["fields"]["components"].First;
			if (components != null && components.HasValues)
				componentsName = components["name"].ToString();
			var severity = jObject["fields"]["customfield_10401"];
			if (severity != null && severity.HasValues)
				severityValue = severity["value"].ToString();
			var summary = jObject["fields"]["summary"];
			if (summary != null && summary.HasValues)
				summaryValue = summary.ToString();
			DefectInfo issueInfo = new DefectInfo();
			issueInfo.Status = statusString;
			issueInfo.Assignee = assigneeName;
			issueInfo.Components = componentsName;
			issueInfo.Severity = severityValue;
			issueInfo.Summary = summaryValue;
			return issueInfo;
		}
	}
}
