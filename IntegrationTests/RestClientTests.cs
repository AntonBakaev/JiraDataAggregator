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
		private readonly IJiraConfigurationHelper jiraConfiguration = new JiraConfigurationHelper();

		[Test]
		public async Task GetIssueStatus_ValidData_ReturnsSuccess()
		{
			var client = new RestClient(jiraConfiguration);
			var dataObject = await client.Get<object>("issue/ONESCREEN-11682/?fields=status");
			
			Assert.IsNotNull(dataObject);
			Assert.DoesNotThrow(() => JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString());
		}
	}
}
