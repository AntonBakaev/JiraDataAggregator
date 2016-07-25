using Core.Enums;

namespace JiraIssueStatusChecker.Abstract
{
    public interface IJiraIssueStatusChecker
    {
        IssueStatus GetIssueStatus(string issueKey);
    }
}
