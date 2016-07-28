using Core.ViewModels.Interfaces;

namespace Core.Reports.Interfaces
{
	public interface IReporter<in TViewModel> 
		where TViewModel : IViewModel
	{
		void Generate(TViewModel viewModel);
	}
}
