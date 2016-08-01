using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Common.Exceptions;
using Common.Messages;
using Core.Reports.Interfaces;
using Core.ViewModels.Interfaces;
using TemplateHelper;

namespace Core.Reports
{
	public abstract class AutomaticRtfReporterBase<TViewModel> : IRtfReporter<TViewModel>
		where TViewModel : IViewModel, IReplaceable
	{
		private const string RtfReportFileNameKey = "RtfDefectReportFileName";

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
			string templatePath = string.Empty;

			try
			{
				foreach (var template in templatesLocations)
				{
					string path = String.Format(basePath, template.Value);
					templatePath = path;
					string text;
					using (var streamReader = new StreamReader(path, Encoding.UTF8))
					{
						text = streamReader.ReadToEnd();
					}
					allTemplates.Add(template.Key, text);
				}
			}
			catch (IOException)
			{
				throw new JiraDataAggregatorException(
					string.Format("{0} at {1}",
					JiraDataAggregatorExceptionMessages.FileExceptionMessages.LoadFromTemplateError, templatePath));
			}
			
			return allTemplates;
		}

		private static void GenerateReportFile(string content)
		{
			string rtfReportFileName = ConfigurationManager.AppSettings[RtfReportFileNameKey];
			try
			{
				using (var streamWriter = new StreamWriter(rtfReportFileName))
				{
					streamWriter.Write(content);
				}
			}
			catch (IOException)
			{
				throw new JiraDataAggregatorException(
					string.Format("{0} at {1}",
					JiraDataAggregatorExceptionMessages.FileExceptionMessages.WriteToFileError, rtfReportFileName));
			}
		}
	}
}
