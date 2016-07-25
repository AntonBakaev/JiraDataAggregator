using System.Collections.Generic;
using Core.Models;

namespace Core.VmBuilders.Interfaces
{
	public interface IAllBlockingDefectsVmBuilder
	{
		IEnumerable<string> GetAllBlockingDefects(List<Execution> executionsList);
	}
}
