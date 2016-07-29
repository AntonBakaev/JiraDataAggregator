using Core.Reports.Interfaces;
using Core.ViewModels.Interfaces;

namespace Core.Reports
{
	public abstract class RtfReporterBase<TViewModel> : IReporter<TViewModel>
		where TViewModel : IViewModel, new()
	{
		protected abstract string RtfReportFileConfigKey { get; }

		public abstract void Generate(TViewModel reportVm);
	}
}
