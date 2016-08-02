using System;
using System.Configuration;
using System.Linq;
using Common.Exceptions;
using Common.Helpers;
using Common.Messages;
using DataAccess.RestServices.JiraConnectionConfig;
using DataAccess.RestServices.RestServiceConfig;

namespace DataAccess.RestServices
{
	public static class RestServicesHelper
	{
		private const string RestServicesSectionName = "restServices";
		private const string JiraConnectionsSectionName = "jiraConnections";

		public static string GetServiceUrl(string serviceName)
		{
			return GetService(serviceName).Url;
		}

		public static string GetJiraConnectionAuthData(string serviceName)
		{
			JiraConnectionElement connection = GetConnection(GetService(serviceName).EndPointName);
			string authData = String.Format("{0}:{1}", connection.Login, connection.Password);

			return ConvertHelper.ToBase64String(authData);
		}

		public static string GetJiraConnectionBaseUrl(string serviceName)
		{
			return GetConnection(GetService(serviceName).EndPointName).BaseUrl;
		}

		private static RestServiceElement GetService(string serviceName)
		{
			var section = (RestServicesSection)ConfigurationManager.GetSection(RestServicesSectionName);
			if (section == null)
				throw new JiraDataAggregatorException(JiraDataAggregatorExceptionMessages
					.ConfigurationExceptionMessages
					.RestServicesSectionNotFoundError);

			var services = section.Services.Cast<RestServiceElement>();

			RestServiceElement service = services.FirstOrDefault(s => s.Name == serviceName);
			if (service != null)
				return service;

			throw new JiraDataAggregatorException(JiraDataAggregatorExceptionMessages
				.ConfigurationExceptionMessages
				.InvalidServiceNameError);
		}

		private static JiraConnectionElement GetConnection(string endPointName)
		{
			var section = (JiraConnectionsSection)ConfigurationManager.GetSection(JiraConnectionsSectionName);
			if (section == null)
				throw new JiraDataAggregatorException(JiraDataAggregatorExceptionMessages
					.ConfigurationExceptionMessages
					.JiraConnectionsSectionNotFoundError);

			var connections = section.Connections.Cast<JiraConnectionElement>();

			JiraConnectionElement connection = connections.FirstOrDefault(c => c.Name == endPointName);
			if (connection != null)
				return connection;

			throw new JiraDataAggregatorException(JiraDataAggregatorExceptionMessages
				.ConfigurationExceptionMessages
				.InvalidConnectionNameError);
		}
	}
}
