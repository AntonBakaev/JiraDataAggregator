using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Helpers;
using Common.Messages;
using DataAccess.RestServices.Interfaces;
using Newtonsoft.Json;
using NLog;

namespace DataAccess.RestServices
{
	public class RestClient : IRestClient
	{
		private readonly ILogger logger;

		public RestClient(ILogger logger)
		{
			this.logger = logger;
		}

		async public Task<TResponse> Get<TResponse>(string serviceName, object parameters = null) where TResponse : new()
		{
			string authString = RestServicesHelper.GetJiraConnectionAuthData(serviceName);
			var baseAddress = new Uri(RestServicesHelper.GetJiraConnectionBaseUrl(serviceName));
			var serviceUrlTemplate = new UriTemplate(RestServicesHelper.GetServiceUrl(serviceName));
			Uri serviceUrl = serviceUrlTemplate.BindByName(baseAddress, ConvertHelper.ToDictionary(parameters));

			HttpClient client = CreateClient(baseAddress, authString);
			HttpResponseMessage response;
			try
			{
				response = await client.GetAsync(serviceUrl);
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				throw;
			}

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
