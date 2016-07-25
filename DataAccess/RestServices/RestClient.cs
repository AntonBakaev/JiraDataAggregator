using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataAccess.RestServices.Interfaces;
using Newtonsoft.Json;

namespace DataAccess.RestServices
{
	public class RestClient
	{
		private readonly IJiraConfigurationHelper jiraConfiguration;

		public RestClient(IJiraConfigurationHelper jiraConfiguration)
		{
			this.jiraConfiguration = jiraConfiguration;
		}

		//ToDo: serviceUrl should be changed to serviceName after restservice section is added to configuration
		async public Task<TResponse> Get<TResponse>(string serviceUrl, object parameters = null) where TResponse : new()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(jiraConfiguration.GetBaseAddress());
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", jiraConfiguration.GetAuthenticationString());

				HttpResponseMessage response = await client.GetAsync(serviceUrl);
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
