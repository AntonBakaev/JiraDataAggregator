using System;
using Common.Helpers.Interfaces;

namespace JiraDataAggregator.Utilities
{
	public class ConsoleLogger: ILogger
	{
		public void Log(Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
