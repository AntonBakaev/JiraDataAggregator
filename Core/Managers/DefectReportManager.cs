using Core.Managers.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Managers
{
	public class DefectReportManager : IDefectReportManager
	{
	    private readonly IDefectReportRepository defectRepository;

	    public DefectReportManager(IDefectReportRepository repository)
	    {
	        this.defectRepository = repository;
	    }

    }
}
