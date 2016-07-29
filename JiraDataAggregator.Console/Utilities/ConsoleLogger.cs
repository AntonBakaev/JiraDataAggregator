using System;
using Common.Helpers.Interfaces;

namespace JiraDataAggregator.Console.Utilities
{
	public class ConsoleLogger: ILogger
	{
		public void Log(Exception ex)
		{
			System.Console.WriteLine(ex.Message);
		}
	}
}
