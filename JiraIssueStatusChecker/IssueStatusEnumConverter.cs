﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace JiraIssueStatusChecker
{
    static class IssueStatusEnumConverter
    {
         public static IssueStatus ConvertToenum(string issueStatus)
        {
            switch (issueStatus)
            {
                case "Open": 
                    return IssueStatus.Open;
                case "Resolved":
                    return IssueStatus.Resolved;
                case "Closed":
                    return IssueStatus.Closed;
                case "Reopened":
                    return IssueStatus.Reopened;
                case "In progress":
                    return  IssueStatus.InProgress;
                default:
                    throw new Exception("Unexpected status"); //todo use custom exception
            }
        }
    }
}
