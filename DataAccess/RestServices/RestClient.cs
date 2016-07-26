using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Helpers;
using DataAccess.RestServices.Interfaces;
using Newtonsoft.Json;

namespace DataAccess.RestServices
{
	public class RestClient
	{
		//private readonly IJiraConfigurationHelper jiraConfiguration;

		//public RestClient(IJiraConfigurationHelper jiraConfiguration)
		//{
		//	this.jiraConfiguration = jiraConfiguration;
		//}

		//ToDo: serviceUrl should be changed to serviceName after restservice section is added to configuration
		async public Task<TResponse> Get<TResponse>(string serviceUrl, object parameters = null) where TResponse : new()
		{
			using (var client = new HttpClient())
			{
				string baseAddress = RestServicesHelper.GetJiraConnectionBaseUrl("GetIssueStatus");
				string authString = RestServicesHelper.GetJiraConnectionAuthData("GetIssueStatus");
				string queryString = String.Empty;

				//string baseAddress = jiraConfiguration.GetAuthenticationString();
				//string authString = jiraConfiguration.GetBaseAddress();

				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

				if (parameters != null)
				{
					queryString = ConvertHelper.ToQueryString(parameters);
				}

				HttpResponseMessage response = await client.GetAsync(serviceUrl + queryString);
				if (response.IsSuccessStatusCode)
				{
					string jsonResult = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<TResponse>(jsonResult);
				}
			}

			return default(TResponse);
		}
	}
}
