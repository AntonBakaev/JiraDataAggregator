namespace DataAccess.RestServices.Interfaces
{
	public interface IJiraConfigurationHelper
	{
		string GetAuthenticationString();
		string GetBaseAddress();
	}
}
