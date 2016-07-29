using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Core.Reports.Interfaces;
using Core.ViewModels.Interfaces;
using TemplateHelper;

namespace Core.Reports
{
	public abstract class AutomaticRtfReporterBase<TViewModel> : IRtfReporter<TViewModel>
		where TViewModel : IViewModel, IReplaceable
	{
		private const string RtfReportFileNameKey = "RtfReportFileName";

		private readonly InputFormatter inputFormatter;
		private Dictionary<Type, string> allTemplates;

		protected abstract string BasePath { get; }
		protected abstract Dictionary<Type, string> TemplatesLocations { get; }

		protected AutomaticRtfReporterBase(InputFormatter inputFormatter)
		{
			this.inputFormatter = inputFormatter;
		}

		public void Generate(TViewModel viewModel)
		{
			allTemplates = LoadTemplates(TemplatesLocations, BasePath);
			var replacer = new TemplateReplacer(inputFormatter);
			string result = replacer.Replace(viewModel, allTemplates);
			GenerateReportFile(result);
		}

		private static Dictionary<Type, string> LoadTemplates(Dictionary<Type, string> templatesLocations, string basePath)
		{
			var allTemplates = new Dictionary<Type, string>();

			foreach (var template in templatesLocations)
			{
				string path = String.Format(basePath, template.Value);
				string text;
				using (var streamReader = new StreamReader(path, Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}
				allTemplates.Add(template.Key, text);
			}

			return allTemplates;
		}

		private static void GenerateReportFile(string content)
		{
			using (var streamWriter = new StreamWriter(ConfigurationManager.AppSettings[RtfReportFileNameKey]))
			{
				streamWriter.Write(content);
			}
		}
	}
}
