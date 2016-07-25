using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JiraIssueStatusChecker.Abstract;

namespace JiraIssueStatusChecker
{
    class JiraBasicAuthenticationProvider: IJiraBasicAuthenticationProvider
    {
        public JiraBasicAuthenticationProvider(string baseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CheckAuthentication(string username, string password)
        {
            string authString = Base64Encode(username + ":" + password);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

            HttpResponseMessage response = await httpClient.GetAsync(String.Empty);
            if (response.StatusCode != HttpStatusCode.Unauthorized && response.StatusCode != HttpStatusCode.NotFound)
            {
                AuthString = authString;
                return true;
            }
            return false;
        }

        public string AuthString { get; private set; }

        private HttpClient httpClient;

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
