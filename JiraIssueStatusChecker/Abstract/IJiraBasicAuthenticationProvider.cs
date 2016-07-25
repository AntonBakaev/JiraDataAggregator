using System.Threading.Tasks;

namespace JiraIssueStatusChecker.Abstract
{
    public interface IJiraBasicAuthenticationProvider
    {
        Task<bool> CheckAuthentication(string username, string password);
        string AuthString { get; }
    }
}
