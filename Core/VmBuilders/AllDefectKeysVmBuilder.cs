using System;
using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class AllDefectKeysVmBuilder : IAllDefectKeysVmBuilder
	{
		public AllDefectKeysVm GetAllBlockingDefects(IEnumerable<Execution> executionsList)
		{
			var defectsKeysList = new List<string>();
			foreach (var execution in executionsList)
				foreach (var defect in execution.ExecutionDefects)
					if (defect != String.Empty && !defectsKeysList.Contains(defect))
						defectsKeysList.Add(defect);

			return new AllDefectKeysVm() { AllDefectKeys = defectsKeysList };
		}
	}
}
