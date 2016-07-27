using System.Threading.Tasks;
using DataAccess.RestServices;
using DataAccess.RestServices.Interfaces;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
	public class RestClientTests
	{
		//private readonly IJiraConfigurationHelper jiraConfiguration = new JiraConfigurationHelper(); todo cleanup

		[Test]
		public async Task GetIssueStatus_ValidData_ReturnsSuccess()
		{
			var client = new RestClient();
			var dataObject = await client.Get<object>("GetIssueStatus", new { issuekey = "ONESCREEN-12551" });

			Assert.IsNotNull(dataObject);
			Assert.DoesNotThrow(() => JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString());
		}

		[Test]
		public async Task GetIssueStatus_InvalidParameters_Throws()
		{
			var client = new RestClient();
			var dataObject = await client.Get<object>("GetIssueStatus", new { d12 = "ONESCREEN-12551" });

			Assert.IsNotNull(dataObject);
			Assert.DoesNotThrow(() => JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString());
		}
	}
}
