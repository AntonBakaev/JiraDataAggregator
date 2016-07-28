using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class AllDefectKeysVmBuilder : IAllDefectKeysVmBuilder
	{
		public AllDefectKeysVm GetAllBlockingDefects(IEnumerable<Execution> executionsList)
		{
			var defectsKeysList = new List<DefectKeyVm>();
			foreach (var execution in executionsList)
				foreach (var defect in execution.ExecutionDefects)
					if (defect != String.Empty && !defectsKeysList.Exists(d => d.Value == defect))
						defectsKeysList.Add(new DefectKeyVm { Value = defect });

			return new AllDefectKeysVm() { AllDefectKeys = defectsKeysList };
		}
	}
}
