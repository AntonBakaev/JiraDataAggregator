using System;
using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class AllDefectKeysVmBuilder : IAllDefectKeysVmBuilder
	{
		public AllDefectKeysVm GetAllBlockingDefects(IEnumerable<Execution> executionsList, Dictionary<string, DefectInfo> defectInfoList)
		{
			var defectsKeysList = new List<DefectKeyVm>();
			foreach (var execution in executionsList)
				foreach (var defect in execution.ExecutionDefects)
					if (!String.IsNullOrEmpty(defect) && !defectsKeysList.Exists(d => d.Value == defect))
						defectsKeysList.Add(new DefectKeyVm
						{
							Value = defect,
							Assignee = defectInfoList[defect].Assignee,
							Status = defectInfoList[defect].Status,
							Components = defectInfoList[defect].Components,
							Summary = defectInfoList[defect].Summary,
							Severity = defectInfoList[defect].Severity
						});

			return new AllDefectKeysVm() { AllDefectKeys = defectsKeysList };
		}
	}
}
