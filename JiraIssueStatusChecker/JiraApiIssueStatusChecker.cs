using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Core.Enums;
using Core.Exceptions;
using JiraIssueStatusChecker.Interfaces;

namespace JiraIssueStatusChecker
{
	class JiraApiIssueStatusChecker : IJiraIssueStatusChecker
	{
		private string authString;
		private HttpClient httpClient;

		public JiraApiIssueStatusChecker(string authString, string baseAddress)
		{
			this.authString = authString;
			httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri(baseAddress);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public HttpStatusCode GetIssueStatus(string issueKey, out string status)
		{
			status = String.Empty;
			
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

			HttpResponseMessage response = httpClient.GetAsync("issue/" + issueKey + "/?fields=status").Result;
			if (response.IsSuccessStatusCode)
			{
				string jsonResult = response.Content.ReadAsStringAsync().Result;

				status = JObject.Parse(jsonResult)["fields"]["status"]["name"].ToString();

				//return IssueStatusEnumConverter.ConvertToenum(status);
			}
			//throw new JiraDataAggregatorException("Call to API resulted in: " + response.StatusCode);
			return response.StatusCode;
		}
	}
}
