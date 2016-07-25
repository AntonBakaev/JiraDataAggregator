using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace JiraIssueStatusChecker.Abstract
{
    public interface IJiraIssueStatusChecker
    {
        Task<IssueStatus> GetIssueStatus(string issueKey);
    }
}
