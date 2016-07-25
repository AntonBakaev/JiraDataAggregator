namespace JiraIssueStatusChecker.Interfaces
{
	public interface IJiraBasicAuthenticationProvider
	{
		string GetAuthString(string username, string password);
	}
}
