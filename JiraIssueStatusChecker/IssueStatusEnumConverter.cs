using Core.Enums;

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
                case "Done":
                    return IssueStatus.Done;
                default:
                    return IssueStatus.Unknown;
                //throw new JiraDataAggregatorException("Unexpected status"); 
            }
        }
    }
}
