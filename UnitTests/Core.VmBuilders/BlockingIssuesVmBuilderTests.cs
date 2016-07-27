using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Models;
using Core.ViewModels;
using Core.VmBuilders;
using Core.VmBuilders.Interfaces;
using NUnit.Framework;

namespace UnitTests.Core.VmBuilders
{
	[TestFixture]
	public class BlockingIssuesVmBuilderTests
	{
		private List<Execution> executionsList;
		private IBlockingIssuesVmBuilder blockingIssuesVmBuilder;

		[SetUp]
		public void SetUp()
		{
			InitializeExecutionsList();
			blockingIssuesVmBuilder = new BlockingIssuesVmBuilder();
		}

		[Test]
		public void GetTopBlockingIssues_GoodInput_ReturnsExpectedVm()
		{
			var expectedVm = new BlockingIssuesVm();
			var expectedList = new List<DefectVm>();
			string baseLinkFormat = "https://telenor-ose.atlassian.net/browse/{0}";

			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-11608",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-11608",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   { 
															IssueName = "ONESCREEN-10865", 
															Link = String.Format(baseLinkFormat, "ONESCREEN-10865")
													   },
													   new IssueVm()
													   {
														    IssueName = "ONESCREEN-10873",
														    Link = String.Format(baseLinkFormat, "ONESCREEN-10873")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-11979",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-11979",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-10865",
															Link = String.Format(baseLinkFormat, "ONESCREEN-10865")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-11788",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-11788",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-11264",
															Link = String.Format(baseLinkFormat, "ONESCREEN-11264")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-12481",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-12481",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-11368",
															Link = String.Format(baseLinkFormat, "ONESCREEN-11368")
													   },
													   new IssueVm()
													   {
														    IssueName = "ONESCREEN-11239",
														    Link = String.Format(baseLinkFormat, "ONESCREEN-11239")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-11770",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-11770",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-10873",
															Link = String.Format(baseLinkFormat, "ONESCREEN-10873")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-11779",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-11779",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-11239",
															Link = String.Format(baseLinkFormat, "ONESCREEN-11239")
													   }}
			});
			expectedList.Add(new DefectVm()
			{
				DefectName = "ONESCREEN-12058",
				Link = "https://telenor-ose.atlassian.net/browse/ONESCREEN-12058",
				BlockingIssues = new List<IssueVm>() { new IssueVm()
													   {
															IssueName = "ONESCREEN-11239",
															Link = String.Format(baseLinkFormat, "ONESCREEN-11239")
													   }}
			});

			expectedVm.DefectsList = expectedList.Take(5)
												 .OrderByDescending(d => d.BlockingIssues.Count)
												 .ToList();

			BlockingIssuesVm actualVm = blockingIssuesVmBuilder.GetTopBlockingIssues(executionsList, 5);

			Assert.IsTrue(IsBlockingIssuesVmsEquivalent(expectedVm, actualVm));
		}

		private void InitializeExecutionsList()
		{
			executionsList = new List<Execution>();
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

		private bool IsBlockingIssuesVmsEquivalent(BlockingIssuesVm expectedVm, BlockingIssuesVm actualVm)
		{
			if (expectedVm.DefectsList.Count() != actualVm.DefectsList.Count())
				return false;

			for (int i = 0; i < expectedVm.DefectsList.Count(); i++)
				if (!IsDefectVmsEquivalent(expectedVm.DefectsList.ElementAt(i), actualVm.DefectsList.ElementAt(i)))
					return false;

			return true;
		}

		private bool IsDefectVmsEquivalent(DefectVm expectedDefectVm, DefectVm actualDefectVm)
		{
			if (expectedDefectVm.DefectName != actualDefectVm.DefectName
				|| expectedDefectVm.Link != actualDefectVm.Link
				|| expectedDefectVm.BlockingIssues.Count != actualDefectVm.BlockingIssues.Count)
				return false;

			for (int i = 0; i < expectedDefectVm.BlockingIssues.Count; i++)
				if (!IsIssueVmsEquivalent(expectedDefectVm.BlockingIssues[i], actualDefectVm.BlockingIssues[i]))
					return false;

			return true;
		}

		private bool IsIssueVmsEquivalent(IssueVm expectedIssueVm, IssueVm actualIssueVm)
		{
			if (expectedIssueVm.IssueName != actualIssueVm.IssueName
			    || expectedIssueVm.Link != actualIssueVm.Link)
				return false;

			return true;
		}
	}
}
