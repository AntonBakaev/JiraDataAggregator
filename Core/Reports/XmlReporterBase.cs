using System.IO;
using Common.Helpers.Interfaces;
using Core.Reports.Interfaces;
using Core.ViewModels.Interfaces;

namespace Core.Reports
{
	public abstract class XmlReporterBase<TViewModel> : IReporter<TViewModel>
		where TViewModel : IViewModel, new()
	{
		private readonly ISerializeHelper<TViewModel> serializeHelper;

		protected XmlReporterBase(ISerializeHelper<TViewModel> serializeHelper)
		{
			this.serializeHelper = serializeHelper;
		}

		public abstract string FileName { get; }

		public virtual void Generate(TViewModel viewModel)
		{
			if (File.Exists(FileName))
				File.Delete(FileName);

			serializeHelper.Serialize(FileName, viewModel);
		}
	}
}
