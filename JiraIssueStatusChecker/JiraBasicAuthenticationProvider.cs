using System;
using JiraIssueStatusChecker.Interfaces;

namespace JiraIssueStatusChecker
{
	class JiraBasicAuthenticationProvider : IJiraBasicAuthenticationProvider
	{
		public string GetAuthString(string username, string password)
		{
			return Base64Encode(String.Format(username + ":" + password));
		}

		private string Base64Encode(string plainText)
		{
			byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}
	}
}
