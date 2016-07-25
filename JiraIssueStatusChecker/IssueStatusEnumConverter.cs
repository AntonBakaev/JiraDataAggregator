using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Exceptions;

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
                case "In Progress":
                    return  IssueStatus.InProgress;
                case "Dones":
                    return IssueStatus.Done;
                default:
                    return IssueStatus.Unknown;
                //throw new JiraDataAggregatorException("Unexpected status"); 
            }
        }
    }
}
