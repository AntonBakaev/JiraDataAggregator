using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Helpers.Interfaces;
using Core.Aggregators.Interfaces;
using Core.Enums;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Core.Aggregators
{
	public class DefectReportAggregator : IDefectReportAggregator
	{
		private readonly IDefectReportRepository defectReportRepository;
		private ILogger logger;

		private Dictionary<string, IssueStatus> issueStatuses;

		public DefectReportAggregator(IDefectReportRepository defectReportRepository, ILogger logger)
		{
			this.defectReportRepository = defectReportRepository;
			this.logger = logger;
			issueStatuses = new Dictionary<string, IssueStatus>();
		}


		public async Task<IEnumerable<Execution>> GetIsitLaunchCriticalViewData(string fileName)
		{
			IEnumerable<Execution> executions = defectReportRepository.GetIsitLaunchCriticalViewData(fileName);
			
			return await FilterExecutionDefects(executions);
		}

		private async Task<IEnumerable<Execution>> FilterExecutionDefects(IEnumerable<Execution> executions)
		{
			List<Task<Tuple<string, IssueStatus>>> tasks = new List<Task<Tuple<string, IssueStatus>>>();
			List<string> scheduledForCheckingIssueKeysList = new List<string>();

			//Stopwatch sw = new Stopwatch();
			//sw.Start();
			
			foreach (Execution execution in executions)
			{
				foreach (string executionDefect in execution.ExecutionDefects)
				{
					if (!scheduledForCheckingIssueKeysList.Contains(executionDefect))
					{
						scheduledForCheckingIssueKeysList.Add(executionDefect);
						tasks.Add(GetIssueStatusWrapper(executionDefect));
					}
				}
			}

			foreach (var task in await Task.WhenAll(tasks))
			{
				string executionDefect = task.Item1;
				IssueStatus executionDefectStatus = task.Item2;
				if (!issueStatuses.ContainsKey(executionDefect))
				{
					issueStatuses.Add(executionDefect, executionDefectStatus);
				}
			}

			foreach (Execution execution in executions)
			{
				List<string> filteredExecutionDefects = new List<string>();
				foreach (string executionDefect in execution.ExecutionDefects)
				{
					if (issueStatuses[executionDefect] != IssueStatus.Done)
					{
						filteredExecutionDefects.Add(executionDefect);
					}
				}
				execution.ExecutionDefects = filteredExecutionDefects;
			}

			return executions;
			//sw.Stop();
		} 

		private async Task<Tuple<string, IssueStatus>> GetIssueStatusWrapper(string issueKey)
		{
			try
			{
				return Tuple.Create(issueKey, await defectReportRepository.GetIssueStatus(issueKey));
			}
			catch (JiraDataAggregatorException ex)
			{
				logger.Log(ex);
			}
			return null;
		}
	}
}
