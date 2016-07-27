using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aggregators.Interfaces;
using Core.Enums;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Core.Aggregators
{
	public class DefectReportAggregator : IDefectReportAggregator
	{
		private readonly IDefectReportRepository defectReportRepository;

		public DefectReportAggregator(IDefectReportRepository defectReportRepository)
		{
			this.defectReportRepository = defectReportRepository;
		}

		public async Task<IEnumerable<Execution>> GetIsitLaunchCriticalViewData(string fileName)
		{
			IEnumerable<Execution> executions = defectReportRepository.GetIsitLaunchCriticalViewData(fileName);


			foreach (Execution execution in executions)
			{
				List<string> filteredExecutionDefects = new List<string>();
				foreach (string executionDefect in execution.ExecutionDefects)
				{
					//todo: there must be issukey validation somewhere
					IssueStatus executionDefectStatus = await defectReportRepository.GetIssueStatus(executionDefect);
					if (executionDefectStatus != IssueStatus.Done)
					{
						filteredExecutionDefects.Add(executionDefect);
					}

				}
				execution.ExecutionDefects = filteredExecutionDefects;
			}
			return executions;
		}
	}
}
