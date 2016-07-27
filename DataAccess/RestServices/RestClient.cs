using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Helpers;
using Newtonsoft.Json;

namespace DataAccess.RestServices
{
	public class RestClient
	{
		async public Task<TResponse> Get<TResponse>(string serviceName, object parameters = null) where TResponse : new()
		{
			string authString = RestServicesHelper.GetJiraConnectionAuthData(serviceName);
			Uri baseAddress = new Uri(RestServicesHelper.GetJiraConnectionBaseUrl(serviceName));
			var serviceUrlTemplate = new UriTemplate(RestServicesHelper.GetServiceUrl(serviceName));
			Uri serviceUrl = serviceUrlTemplate.BindByName(baseAddress, ConvertHelper.ToDictionary(parameters));

			HttpClient client = CreateClient(baseAddress, authString);
			
			HttpResponseMessage response = await client.GetAsync(serviceUrl);


			if (!response.IsSuccessStatusCode)
			{
				throw new JiraDataAggregatorException(String.Format("{0} at {1}",
					JdaExceptionHelper.GetSpecificRestException(response.StatusCode), serviceUrl.AbsoluteUri));
			}
			
			string jsonResult = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TResponse>(jsonResult);
		}

		private HttpClient CreateClient(Uri baseAddress, string authString)
		{
			var client = new HttpClient();
			client.BaseAddress = baseAddress;
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

			return client;
		}
	}
}
