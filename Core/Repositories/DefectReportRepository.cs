using Core.Repositories.Interfaces;

namespace Core.Repositories
{
	public class DefectReportRepository : IDefectReportRepository
	{
        public string GetMessage()
        {
            return "Hello, world!";
        }
    }
}
