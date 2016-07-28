using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Helpers;
using Common.Messages;
using Newtonsoft.Json;
using NLog;

namespace DataAccess.RestServices
{
	public class RestClient
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		async public Task<TResponse> Get<TResponse>(string serviceName, object parameters = null) where TResponse : new()
		{
			string authString = RestServicesHelper.GetJiraConnectionAuthData(serviceName);
			Uri baseAddress = new Uri(RestServicesHelper.GetJiraConnectionBaseUrl(serviceName));
			var serviceUrlTemplate = new UriTemplate(RestServicesHelper.GetServiceUrl(serviceName));
			Uri serviceUrl = serviceUrlTemplate.BindByName(baseAddress, ConvertHelper.ToDictionary(parameters));

			HttpClient client = CreateClient(baseAddress, authString);
			
			HttpResponseMessage response = await client.GetAsync(serviceUrl);

			logger.Info("{0} to {1}", ConnectionMessages.SuccessfulRequestSent, serviceUrl.AbsoluteUri);

			if (!response.IsSuccessStatusCode)
			{
				var message = String.Format("{0} at {1}",
					JdaException.GetSpecificRestException(response.StatusCode), serviceUrl.AbsoluteUri);
				logger.Error(message);
				throw new JiraDataAggregatorException(message);
			}
			
			string jsonResult = await response.Content.ReadAsStringAsync();

			logger.Info("{0} from {1}", ConnectionMessages.SuccessfulResponseReceived, serviceUrl.AbsoluteUri);

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
