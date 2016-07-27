using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Helpers;
using Newtonsoft.Json;

namespace DataAccess.RestServices
{
	public class RestClient
	{
		async public Task<TResponse> Get<TResponse>(string serviceName, object parameters = null) where TResponse : new()
		{
			using (var client = new HttpClient())
			{
				string authString = RestServicesHelper.GetJiraConnectionAuthData(serviceName);
				Uri baseAddress = new Uri(RestServicesHelper.GetJiraConnectionBaseUrl(serviceName));
				UriTemplate serviceUrlTemplate = new UriTemplate(RestServicesHelper.GetServiceUrl(serviceName));
				//todo: rethrow argumentException
				Uri serviceUrl = serviceUrlTemplate.BindByName(baseAddress, ConvertHelper.ToDictionary(parameters)); 

				client.BaseAddress = baseAddress;
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
				
				//todo: rethrow possible exception
				HttpResponseMessage response = await client.GetAsync(serviceUrl); 
				if (!response.IsSuccessStatusCode)
				{
					throw JiraDataAggragatorRestExceptionFactory.GetSpecificRestException(response.StatusCode, serviceUrl.AbsoluteUri);
				}

				string jsonResult = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(jsonResult);
			}

			//return default(TResponse);
		}
	}
}
