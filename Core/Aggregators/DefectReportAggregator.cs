using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Helpers;
using Core.Aggregators.Interfaces;
using Core.Enums;
using Core.Models;
using Core.Repositories.Interfaces;
using NLog;

namespace Core.Aggregators
{
	public class DefectReportAggregator : IDefectReportAggregator
	{
		private readonly IDefectReportRepository defectReportRepository;
		private readonly ILogger logger;

		 // todo rename
		private Dictionary<string, DefectInfo> defectInfos;

		public DefectReportAggregator(IDefectReportRepository defectReportRepository, ILogger logger)
		{
			this.defectReportRepository = defectReportRepository;
			defectInfos = new Dictionary<string, DefectInfo>();
			this.logger = logger;
		}

		public IEnumerable<Execution> GetExecutions(string fileName)
		{
			return defectReportRepository.GetIsitLaunchCriticalViewData(fileName);
		}

		public async Task<Dictionary<string, DefectInfo>> GetExecutionsDefectInfo(IEnumerable<Execution> executions)
		{
			List<Task<Tuple<string, DefectInfo>>> tasks = new List<Task<Tuple<string, DefectInfo>>>();
			List<string> scheduledForCheckingIssueKeysList = new List<string>();

			foreach (var execution in executions)
			{
				foreach (var executionDefect in execution.ExecutionDefects)
				{
					if (!scheduledForCheckingIssueKeysList.Contains(executionDefect))
					{
						scheduledForCheckingIssueKeysList.Add(executionDefect);
						tasks.Add(GetIssueInfoWrapper(executionDefect));
					}
				}
			}

			foreach (var task in await Task.WhenAll(tasks))
			{
				string executionDefect = task.Item1;
				DefectInfo executionDefectInfo = task.Item2;
				if (!defectInfos.ContainsKey(executionDefect))
				{
					defectInfos.Add(executionDefect, executionDefectInfo);
				}
			}

			return defectInfos;
			//sw.Stop();
		}

		public IEnumerable<Execution> Filter(IEnumerable<Execution> executions, Dictionary<string, DefectInfo> infos)
		{
			foreach (var execution in executions)
			{
				var filteredExecutionDefects = new List<string>();
				foreach (var executionDefect in execution.ExecutionDefects)
				{
					if (ConvertHelper.ToEnum<IssueStatus>(infos[executionDefect].Status) != IssueStatus.Done)
					{
						filteredExecutionDefects.Add(executionDefect);
					}
				}
				execution.ExecutionDefects = filteredExecutionDefects;
			}
			return executions;
		}

		private async Task<Tuple<string, IssueStatus>> GetIssueStatusWrapper(string issueKey)
		{
			try
			{
				return Tuple.Create(issueKey, await defectReportRepository.GetIssueStatus(issueKey));
			}
			catch (JiraDataAggregatorException)
			{
				logger.Error(JdaException.GetSpecificRestException(0));
			}
			return null;
		}

		private async Task<Tuple<string, DefectInfo>> GetIssueInfoWrapper(string issueKey)
		{
			try
			{
				return Tuple.Create(issueKey, await defectReportRepository.GetIssueInfo(issueKey));
			}
			catch (JiraDataAggregatorException)
			{
				logger.Error(JdaException.GetSpecificRestException(0));
				throw;
			}
		}
	}
}
