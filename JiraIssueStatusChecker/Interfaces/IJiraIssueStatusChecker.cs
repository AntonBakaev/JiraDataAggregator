﻿using System.Net;
using Core.Enums;

namespace JiraIssueStatusChecker.Interfaces
{
	public interface IJiraIssueStatusChecker
	{
		//IssueStatus GetIssueStatus(string issueKey);
		HttpStatusCode GetIssueStatus(string issueKey, out string status);
	}
}
