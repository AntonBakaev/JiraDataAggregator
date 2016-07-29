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
		private const string BaseLinkFormatConfigKey = "BaseLinkFormat";

		public BlockingIssuesVm GetTopBlockingIssues(IEnumerable<Execution> executionsList, int numberOfTopBlockingIssues)
		{
			var defectsList = GetDefectsList(executionsList);

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

			return new BlockingIssuesVm()
			{
				DefectsList = defectsVmList.Take(numberOfTopBlockingIssues)
										   .OrderByDescending(d => d.BlockingIssues.Count)
										   .ToList()
			};
		}

		private List<Defect> GetDefectsList(IEnumerable<Execution> executionsList)
		{
			var defectsList = new List<Defect>();

			foreach (var issue in executionsList)
			{
				foreach (var defectKey in issue.ExecutionDefects)
				{
					if (defectKey == String.Empty)
						continue;

					int defectIndex = -1;
					if ((defectIndex = defectsList.FindIndex(d => d.Key == defectKey)) != -1)
						defectsList[defectIndex].BlockingIssuesKeys.Add(issue.IssueKey);
					else
						defectsList.Add(new Defect() { Key = defectKey, BlockingIssuesKeys = new List<string>() { issue.IssueKey } });
				}
			}

			return defectsList;
		}
	}
}
