using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;

namespace Core.VmBuilders.Interfaces
{
	public interface IAllDefectKeysVmBuilder
	{
		AllDefectKeysVm GetAllBlockingDefects(IEnumerable<Execution> executionsList, Dictionary<string, DefectInfo> defectInfoList);
	}
}
