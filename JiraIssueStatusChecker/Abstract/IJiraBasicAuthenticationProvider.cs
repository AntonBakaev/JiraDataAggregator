namespace JiraIssueStatusChecker.Abstract
{
    public interface IJiraBasicAuthenticationProvider
    {
        string GetAuthString(string username, string password);
    }
}
