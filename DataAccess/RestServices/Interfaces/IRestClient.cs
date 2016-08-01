using System.Threading.Tasks;

namespace DataAccess.RestServices.Interfaces
{
	public interface IRestClient
	{
		Task<TResponse> Get<TResponse>(string serviceName, object parameters = null) where TResponse : new();
	}
}
