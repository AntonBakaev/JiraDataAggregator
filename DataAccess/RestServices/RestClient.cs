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
		async public Task<TResponse> Get<TResponse>(string serviceName, string[] urlParameters = null, object queryStringParameters = null) where TResponse : new()
		{
			using (var client = new HttpClient())
			{
				string authString = RestServicesHelper.GetJiraConnectionAuthData(serviceName);
				string baseAddress = RestServicesHelper.GetJiraConnectionBaseUrl(serviceName);
				string serviceUrlFormatString = RestServicesHelper.GetServiceUrl(serviceName);

				string serviceUrl = String.Empty;
				string queryString = String.Empty;


				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

				if (urlParameters != null)
				{
					serviceUrl = String.Format(serviceUrlFormatString, urlParameters);
				}

				if (queryStringParameters != null)
				{
					queryString = ConvertHelper.ToQueryString(queryStringParameters);
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
