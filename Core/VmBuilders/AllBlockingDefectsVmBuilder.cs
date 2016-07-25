using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class AllBlockingDefectsVmBuilder : IAllBlockingDefectsVmBuilder
	{
		public IEnumerable<string> GetAllBlockingDefects(List<Execution> executionsList)
		{
			var defectsKeysList = new List<string>();
			foreach (var execution in executionsList)
				foreach (var defect in execution.ExecutionDefects)
					defectsKeysList.Add(defect);
			return defectsKeysList.Distinct();
		}
	}
}
