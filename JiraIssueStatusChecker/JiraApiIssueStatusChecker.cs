using System;
using System.Threading.Tasks;
using JiraIssueStatusChecker.Abstract;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Core.Enums;
using Core.Exceptions;

namespace JiraIssueStatusChecker
{
    class JiraApiIssueStatusChecker: IJiraIssueStatusChecker
    {
        private IJiraBasicAuthenticationProvider authenticationProvider;
        private HttpClient httpClient;

        //todo base address should be "https://telenor-ose.atlassian.net/rest/api/2/" and moved to config file
        public JiraApiIssueStatusChecker(IJiraBasicAuthenticationProvider authenticationProvider, string baseAddress)
        {
            this.authenticationProvider = authenticationProvider;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IssueStatus> GetIssueStatus(string issueKey)
        {
            string authString = authenticationProvider.AuthString;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            HttpResponseMessage response = await httpClient.GetAsync("issue/" + issueKey + "/?fields=status"); //use result and remove async?
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();

                string status = JObject.Parse(jsonResult)["fields"]["status"]["name"].ToString();

                return IssueStatusEnumConverter.ConvertToenum(status);
            }
            throw new JiraDataAggregatorException("Call to API resulted in: " + response.StatusCode);
        }
    }
}
