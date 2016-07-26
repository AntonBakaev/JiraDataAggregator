using System;
using System.Collections.Generic;
using System.Globalization;
using Common.Comparators;
using Common.Helpers;
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
			object testObject = new {intValue = 123, stringValue = "abcd"};
			
			var expectedDict = new Dictionary<string, string>
			{
				{"intValue", "123"},
				{"stringValue", "abcd"},
			};

			var actualDict = ConvertHelper.ToDictionary(testObject);

			Assert.IsTrue(DictionaryComparer.Equals(actualDict, expectedDict));
		}
	}
}
