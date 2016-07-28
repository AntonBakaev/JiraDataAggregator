using Core.ViewModels.Interfaces;

namespace Core.Reports.Interfaces
{
	public interface IRtfReporter<in TViewModel> : IReporter<TViewModel> 
		where TViewModel : IViewModel
	{
	}
}
