using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TemplateHelper.UnitTests
{
	[TestFixture]
	public class TemplateReplacerTests
	{
		#region Models

		class IssueVm : IReplaceable
		{
			public string Name { get; set; }
			public int Count { get; set; }
		}

		class IssueDependencyVm : IReplaceable
		{
			public IssueVm Issue { get; set; }
			public List<IssueVm> DepIssueList { get; set; }
		}

		private readonly IssueVm Issue1 = new IssueVm {Name = "Os1", Count = 3};
		private readonly IssueVm Issue2 = new IssueVm { Name = "Os2", Count = 1 };
		private readonly IssueVm Issue3 = new IssueVm { Name = "Os3", Count = 5 };
		private readonly IssueVm Issue4 = new IssueVm { Name = "Os4", Count = 2 };

		#endregion

		private readonly ITemplateReplacer replacer = new TemplateReplacer();

		[Test]
		public void Replace_TwoValidTemplates_Success()
		{
			var depIssue = new IssueDependencyVm
			{
				Issue = Issue1, 
				DepIssueList = new List<IssueVm> { Issue3, Issue2, Issue4 }
			};
			Dictionary<Type, string> allTemplates = new Dictionary<Type, string>
			{
				{typeof(IssueVm), @"Description: Issue {Name} Total count {Count} \n"},
				{typeof(IssueDependencyVm), @"Static text \n {Issue} : {DepIssueList}"}
			};

			string expectedResult = @"Static text \n Description: Issue Os1 Total count 3 \n : Description: "+
				@"Issue Os3 Total count 5 \nDescription: Issue Os2 Total count 1 \nDescription: Issue Os4 Total count 2 \n";

			string actualResult = replacer.Replace(depIssue, allTemplates);

			Assert.AreEqual(actualResult, expectedResult);
		}
	}
}
