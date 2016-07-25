﻿using System.Collections.Generic;
using Core.Models;
using Core.ViewModels;

namespace Core.VmBuilders.Interfaces
{
	public interface IBlockingIssuesVmBuilder
	{
		IEnumerable<DefectVm> GetTopBlockingIssues(IEnumerable<Execution> executionsList, int numberOfBlockingIssues);
	}
}