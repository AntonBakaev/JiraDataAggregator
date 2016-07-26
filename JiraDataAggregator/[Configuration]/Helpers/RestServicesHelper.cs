using System;
using System.Configuration;
using System.Linq;
using JiraDataAggregator._Configuration_.JiraConnection;
using JiraDataAggregator._Configuration_.RestService;

namespace JiraDataAggregator._Configuration_.Helpers
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
				throw new Exception("RestServices section not found.");
			var services = section.Services.Cast<RestServiceElement>();
			RestServiceElement service;
			if ((service = services.FirstOrDefault(s => s.Name == serviceName)) != null)
				return service;
			throw new Exception("Service name is invalid.");
		}

		private static JiraConnectionElement GetConnection(string endPointName)
		{
			var section = (JiraConnectionsSection)ConfigurationManager.GetSection(JiraConnectionsSectionName);
			if (section == null)
				throw new Exception("JiraConnections section not found.");
			var connections = section.Connections.Cast<JiraConnectionElement>();
			JiraConnectionElement connection;
			if ((connection = connections.FirstOrDefault(c => c.Name == endPointName)) != null)
				return connection;
			throw new Exception("Connection name is invalid.");
		}
	}
}
