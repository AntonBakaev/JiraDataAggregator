using System;
using System.Configuration;
using Common.Helpers;
using DataAccess.RestServices.Interfaces;

namespace DataAccess.RestServices
{
	public class JiraConfigurationHelper : IJiraConfigurationHelper
	{
		private const string JiraApiBaseString = "JiraApiBaseString";
		private const string JiraLogin = "JiraLogin";
		private const string JiraPassword = "JiraPassword";

		public string GetAuthenticationString()
		{
			string authSettings = String.Format("{0}:{1}", ConfigurationManager.AppSettings[JiraLogin],
				ConfigurationManager.AppSettings[JiraPassword]);

			return StringExtensions.ToToBase64String(authSettings);
		}

		public string GetBaseAddress()
		{
			return ConfigurationManager.AppSettings[JiraApiBaseString];
		}
	}
}
