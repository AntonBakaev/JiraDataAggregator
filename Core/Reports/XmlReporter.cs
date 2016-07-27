using Common.Helpers;
using Core.ViewModels.Interfaces;

namespace Core.Reports
{
	public class XmlReporter<TViewModel> where TViewModel : IViewModel, new()
	{
		protected virtual void Generate(string fileNameToGenerate, IViewModel viewModel)
		{
			SerializeHelper<TViewModel>.Serialize(fileNameToGenerate, viewModel);
		}
	}
}
