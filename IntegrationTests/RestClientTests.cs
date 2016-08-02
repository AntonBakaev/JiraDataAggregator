using System.Threading.Tasks;
using DataAccess.RestServices;
using Newtonsoft.Json.Linq;
using NLog;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
	public class RestClientTests
	{
		[Test]
		public async Task GetIssueStatus_ValidData_ReturnsSuccess()
		{
			var client = new RestClient(LogManager.GetCurrentClassLogger());
			var dataObject = await client.Get<object>("GetIssueStatus", new { issuekey = "ONESCREEN-11682" });

			Assert.IsNotNull(dataObject);
			Assert.DoesNotThrow(() => JObject.FromObject(dataObject)["fields"]["status"]["name"].ToString());
		}
	}
}
