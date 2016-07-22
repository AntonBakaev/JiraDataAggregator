using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string GetMessage()
        {
            return this.defectRepository.GetMessage();
        }
    }
}
