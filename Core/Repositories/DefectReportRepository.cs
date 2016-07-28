﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Exceptions;
using Core.Models;
using Core.Repositories.Interfaces;
using Common.Helpers;
using Common.Helpers.Interfaces;
using Core.Enums;
using DataAccess.RestServices;
using Newtonsoft.Json.Linq;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
		// todo: consider using interface
		private RestClient restClient;
		private readonly ISerializeHelper<List<Execution>> serializeHelper;

		public DefectReportRepository(RestClient restClient, ISerializeHelper<List<Execution>> serializeHelper)
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
	}
}