using Core.Enums;

namespace JiraIssueStatusChecker.Interfaces
{
    public interface IJiraIssueStatusChecker
    {
        IssueStatus GetIssueStatus(string issueKey);
    }
}
