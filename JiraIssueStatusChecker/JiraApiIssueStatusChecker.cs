using System;
using JiraIssueStatusChecker.Abstract;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Core.Enums;
using Core.Exceptions;

namespace JiraIssueStatusChecker
{
    class JiraApiIssueStatusChecker : IJiraIssueStatusChecker
    {
        public JiraApiIssueStatusChecker(string authString, string baseAddress)
        {
            this.authString = authString;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IssueStatus GetIssueStatus(string issueKey)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            HttpResponseMessage response = httpClient.GetAsync("issue/" + issueKey + "/?fields=status").Result; //?
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;

                string status = JObject.Parse(jsonResult)["fields"]["status"]["name"].ToString();

                return IssueStatusEnumConverter.ConvertToenum(status);
            }
            throw new JiraDataAggregatorException("Call to API resulted in: " + response.StatusCode);
        }

        private string authString;
        private HttpClient httpClient;
    }
}
