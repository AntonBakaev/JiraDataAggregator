using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels;
using Core.ViewModels.Interfaces;

namespace Core.Reports
{
    public class RtfReporter<TViewModel> where TViewModel : IViewModel, new()
    {
        private const string RtfReportFileConfigKey = "RtfDefectReportFileName";

        public virtual void Generate(IViewModel defectReportVm)
        {
        }
    }
}
