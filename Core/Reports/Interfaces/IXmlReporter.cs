using Core.ViewModels.Interfaces;

namespace Core.Reports.Interfaces
{
	public interface IXmlReporter<in TViewModel> : IReporter<TViewModel>
		where TViewModel : IViewModel
	{
	}
}
