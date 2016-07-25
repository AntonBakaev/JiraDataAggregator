using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class BlockingIssuesVmBuilder : IBlockingIssuesVmBuilder
	{
		public IEnumerable<DefectVm> GetTopBlockingIssues(List<Execution> executionsList, int numberOfTopBlockingIssues)
		{
			var defectsList = GetDefectsList(GetIssuesList(executionsList));

			string baseLinkFormat = ConfigurationManager.AppSettings["BaseLinkFormat"];

			var defectsVmList = new List<DefectVm>();
			 foreach (var defect in defectsList)
			{
				var issuesVmList = new List<IssueVm>();
				foreach (var issueKey in defect.BlockingIssuesKeys)
					issuesVmList.Add(new IssueVm() { IssueName = issueKey, Link = String.Format(baseLinkFormat, issueKey) });

				defectsVmList.Add(new DefectVm()
				{
					DefectName = defect.Key,
					Link = String.Format(baseLinkFormat, defect.Key),
					BlockingIssues = issuesVmList
				});
			}
			return defectsVmList.Take(numberOfTopBlockingIssues).OrderByDescending(d => d.BlockingIssues.Count).ToList();
		}

		private List<Issue> GetIssuesList(List<Execution> executionsList)
		{
			var issuesList = new List<Issue>();
			foreach (var execution in executionsList)
				issuesList.Add(new Issue() { Key = execution.IssueKey, ExecutedDeffectsKeys = execution.ExecutionDefects.ToList() });
			return issuesList;
		}

		private List<Defect> GetDefectsList(List<Issue> issuesList)
		{
			var defectsList = new List<Defect>();
			foreach (var issue in issuesList)
			{
				foreach (var defectKey in issue.ExecutedDeffectsKeys)
				{
					if (defectKey == String.Empty)
						continue;
					int defectIndex = -1;
					if ((defectIndex = defectsList.FindIndex(d => d.Key == defectKey)) != -1)
						defectsList[defectIndex].BlockingIssuesKeys.Add(issue.Key);
					else
						defectsList.Add(new Defect() { Key = defectKey, BlockingIssuesKeys = new List<string>() { issue.Key } });
				}
			}
			return defectsList;
		}
	}
}
