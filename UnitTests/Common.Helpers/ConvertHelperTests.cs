using System;
using System.Collections.Generic;
using System.Globalization;
using Common.Comparators;
using Common.Helpers;
using Core.Enums;
using NUnit.Framework;

namespace UnitTests.Common.Helpers
{
	[TestFixture]
	public class ConvertHelperTests
	{
		private readonly IEqualityComparer<Dictionary<string, string>> DictionaryComparer
			= new DictionaryComparer<string, string>(StringComparer.Create(new CultureInfo("en-EN"), false));

		[Test]
		public void ToDictionary_AnonymousObject_ReturnsSuccess()
		{
			object testObject = new { intValue = 123, stringValue = "abcd" };

			var expectedDict = new Dictionary<string, string>
			{
				{"intValue", "123"},
				{"stringValue", "abcd"},
			};

			var actualDict = ConvertHelper.ToDictionary(testObject);

			Assert.IsTrue(DictionaryComparer.Equals(actualDict, expectedDict));
		}

		[Test]
		public void ToEnum_ExistingValue_ReturnsCorrespondingEnum()
		{
			string issueString = "Done";

			IssueStatus issueStatus = ConvertHelper.ToEnum<IssueStatus>(issueString);

			Assert.AreEqual(IssueStatus.Done, issueStatus);
		}

		[Test]
		public void ToEnum_NonExistingValue_ReturnsDefault()
		{
			string issueString = "aDone";

			IssueStatus issueStatus = ConvertHelper.ToEnum<IssueStatus>(issueString);

			Assert.AreEqual(default(IssueStatus), issueStatus);
		}

		[Test]
		public void ToEnumString_ExistingValue_ReturnsCorrespondingValue()
		{
			IssueStatus issueStatus = IssueStatus.InProgress;

			string issueString = ConvertHelper.ToEnumString<IssueStatus>(issueStatus);

			Assert.AreEqual("In progress", issueString);
		}


		[Test]
		public void ToQueryString_CorrectData_ReturnsCorrespondingQueryString()
		{
			object parameters = new
			{
				Property1 = "value1",
				Property2 = "value2"
			};

			string queryString = ConvertHelper.ToQueryString(parameters);
			queryString = queryString.Remove(queryString.Length - 1);

			Assert.AreEqual("?Property1=value1,Property2=value2", queryString);
		}

		[Test]
		public void ToQueryString_Null_ReturnsEmptyString()
		{
			object parameters = null;

			string queryString = ConvertHelper.ToQueryString(parameters);
			//queryString = queryString.Remove(queryString.Length - 1);

			Assert.AreEqual(String.Empty, queryString);
		}
	}
}
