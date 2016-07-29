﻿using System.Collections.Generic;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders;
using Core.VmBuilders.Interfaces;
using NUnit.Framework;

namespace UnitTests.Core.VmBuilders
{
	[TestFixture]
	public class FlowStatisticsVmBuilderTests
	{
		private List<Execution> executionsList;
		private IFlowStatisticsVmBuilder vmBuilder;

		[SetUp]
		public void Init()
		{
			executionsList = new List<Execution>();
			vmBuilder = new FlowStatisticsVmBuilder();

			executionsList.Add(
				new Execution
				{
					ExecutionId = "0001466510652459-242ac112-0001",
					CycleName = "ISIT_HU Launch Critical_Main",
					IssueKey = "ONESCREEN-10865",
					TestSummary = "E2E_CSR_Existing customer buys Phone only + new delivery address, Pay on Delivery",
					Project = "One Screen",
					Versions = "Hungary M10-M11",
					Components = ComponentType.OseFe,
					Priority = PriorityType.P5,
					ExecutedBy = "Natalia Tolstaya",
					ExecutedOn = "Tue Jul 05 05:17:25 PDT 2016",
					ExecutedStatus = ExecutedStatus.Pass,
					AllExecutionDefectsFullString = "ONESCREEN-11608,ONESCREEN-11979 | ONESCREEN-11979",
					CreationDate = "Tue Jun 21 05:04:12 PDT 2016"
				}
				);

			executionsList.Add(
				new Execution
				{
					ExecutionId = "0001466510657872-242ac112-0001",
					CycleName = "ISIT_HU Launch Critical_Main",
					IssueKey = "ONESCREEN-11264",
					TestSummary = "Shop_E2E_Existing online customer buys Phone with accessory, paying on Delivery",
					Project = "One Screen",
					Versions = "Hungary M10-M11",
					Components = ComponentType.OseFe,
					Priority = PriorityType.P2,
					ExecutedBy = "Natalia Tolstaya",
					ExecutedOn = "Wed Jun 29 09:17:36 PDT 2016",
					ExecutedStatus = ExecutedStatus.Pass,
					AllExecutionDefectsFullString = "ONESCREEN-11788 | ONESCREEN-11788",
					CreationDate = "Tue Jun 21 05:04:17 PDT 2016"
				}
				);

			executionsList.Add(
				new Execution
				{
					ExecutionId = "0001466510664995-242ac112-0001",
					CycleName = "ISIT_HU Launch Critical_Main",
					IssueKey = "ONESCREEN-11368",
					TestSummary = "Shop_E2E: Retail Partner sells Tablet to anonym customer selecting Pay On Delivery payment option, Central stock",
					Project = "One Screen",
					Versions = "Hungary M10-M11",
					Components = ComponentType.OseFe,
					Priority = PriorityType.P4,
					ExecutedBy = "Kateryna Andronova",
					ExecutedOn = "Wed Jul 06 08:36:35 PDT 2016",
					ExecutedStatus = ExecutedStatus.Blocked,
					AllExecutionDefectsFullString = "ONESCREEN-12481",
					CreationDate = "Tue Jun 21 05:04:24 PDT 2016"
				}
				);

			executionsList.Add(
				new Execution
				{
					ExecutionId = "0001466510653264-242ac112-0001",
					CycleName = "ISIT_HU Launch Critical_Main",
					IssueKey = "ONESCREEN-10873",
					TestSummary = "E2E_CSR_Existing customer buys MBB Subscription with Dongle, new delivery address, Pay on Delivery, bank transfer",
					Project = "One Screen",
					Versions = "Hungary M10-M11",
					Components = ComponentType.OseFe,
					Priority = PriorityType.P5,
					ExecutedBy = "Mykola Chugunnyi",
					ExecutedOn = "Fri Jul 08 07:37:12 PDT 2016",
					ExecutedStatus = ExecutedStatus.WIP,
					AllExecutionDefectsFullString = "ONESCREEN-11770,ONESCREEN-11608 | ONESCREEN-11770,ONESCREEN-11608",
					CreationDate = "Tue Jun 21 05:04:13 PDT 2016"
				}
				);

			executionsList.Add(
				new Execution
				{
					ExecutionId = "0001466510656990-242ac112-0001",
					CycleName = "ISIT_HU Launch Critical_Main",
					IssueKey = "ONESCREEN-11239",
					TestSummary = "Shop_E2E: Retail sells Phone and accessory to New customer, Central stock, Pay on Delivery",
					Project = "One Screen",
					Versions = "Hungary M10-M11",
					Components = ComponentType.OseFe,
					Priority = PriorityType.P3,
					ExecutedBy = "Natalia Tolstaya",
					ExecutedOn = "Tue Jul 05 04:10:46 PDT 2016",
					ExecutedStatus = ExecutedStatus.Fail,
					AllExecutionDefectsFullString = "ONESCREEN-11779,ONESCREEN-12058,ONESCREEN-12481 | ONESCREEN-11779,ONESCREEN-12058",
					CreationDate = "Tue Jun 21 05:04:16 PDT 2016"
				}
				);
		}

		[Test]
		public void GetFlowStatisticsVm_PassCorrectFile_ReturnsCorrectViewModel()
		{
			var expectedViewModel = new FlowStatisticsVm
			{
				Passed = 2,
				Blocked = 1,
				Wip = 1,
				Failed = 1
			};

			var actualViewModel = vmBuilder.GetFlowStatisticsVm(executionsList);

			Assert.IsTrue(CompareViewModels(expectedViewModel, actualViewModel));
		}

		[Test]
		public void GetFlowStatisticsVmByFilter_PassCorrectData_ReturnsCorrectViewModel()
		{
			var expectedViewModel = new FlowStatisticsVm
			{
				Blocked = 1,
				Failed = 1,
				Wip = 0,
				Passed = 0
			};

			var actualViewmodel = vmBuilder.GetFlowStatisticsVmByFilter(executionsList);

			Assert.IsTrue(CompareViewModels(expectedViewModel, actualViewmodel));
		}

		#region HelperMethods

		private bool CompareViewModels(FlowStatisticsVm expectedViewModel, FlowStatisticsVm actualViewModel)
		{
			return (expectedViewModel.Blocked == actualViewModel.Blocked) &&
				   (expectedViewModel.Failed == actualViewModel.Failed) &&
				   (expectedViewModel.Passed == actualViewModel.Passed) &&
				   (expectedViewModel.Wip == actualViewModel.Wip);
		}

		#endregion HelperMethods
	}
}
