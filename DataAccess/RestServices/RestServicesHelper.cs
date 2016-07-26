using System;
using System.Configuration;
using System.Linq;
using DataAccess.RestServices.JiraConnectionConfig;
using DataAccess.RestServices.RestServiceConfig;

namespace DataAccess.RestServices
{
	public static class RestServicesHelper
	{
		private const string RestServicesSectionName = "services";
		private const string JiraConnectionsSectionName = "connections";

		public static string GetServiceUrl(string serviceName)
		{
			return GetService(serviceName).Url;
		}

		public static string GetJiraConnectionAuthData(string serviceName)
		{
			JiraConnectionElement connection = GetConnection(GetService(serviceName).EndPointName);
			return String.Format("{0}:{1}", connection.Login, connection.Password);
		}

		public static string GetJiraConnectionBaseUrl(string serviceName)
		{
			return GetConnection(GetService(serviceName).EndPointName).BaseUrl;
		}

		private static RestServiceElement GetService(string serviceName)
		{
			var section = (RestServicesSection)ConfigurationManager.GetSection(RestServicesSectionName);
			if (section == null)
				throw new Exception("RestServices section not found."); //todo: use custom exception

			var services = section.Services.Cast<RestServiceElement>();

			RestServiceElement service = services.FirstOrDefault(s => s.Name == serviceName);
			if (service != null)
				return service;

			throw new Exception("Service name is invalid.");//todo: use custom exception
		}

		private static JiraConnectionElement GetConnection(string endPointName)
		{
			var section = (JiraConnectionsSection)ConfigurationManager.GetSection(JiraConnectionsSectionName);
			if (section == null)
				throw new Exception("JiraConnections section not found.");//todo: use custom exception

			var connections = section.Connections.Cast<JiraConnectionElement>();

			JiraConnectionElement connection = connections.FirstOrDefault(c => c.Name == endPointName);
			if (connection != null)
				return connection;

			throw new Exception("Connection name is invalid.");//todo: use custom exception
		}
	}
}
