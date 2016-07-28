﻿using Common.Helpers.Interfaces;
using Core.ViewModels.Interfaces;

namespace Core.Reports
{
	public abstract class XmlReporterBase<TViewModel> where TViewModel : IViewModel, new()
	{
		private readonly ISerializeHelper<TViewModel> serializeHelper;

		protected XmlReporterBase(ISerializeHelper<TViewModel> serializeHelper)
		{
			this.serializeHelper = serializeHelper;
		}

		public abstract string FileName { get; }

		public virtual void Generate(TViewModel viewModel)
		{
			serializeHelper.Serialize(FileName, viewModel);
		}
	}
}
