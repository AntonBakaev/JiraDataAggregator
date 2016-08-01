using System;
using Common.Utilities.Interfaces;
using NLog;

namespace Common.Utilities
{
	public class LoggerFactory : ILoggerFactory
	{
		private const string Logger = "Logger";

		public ILogger Create(Type type)
		{
			return LogManager.GetLogger(Logger);
		}
	}
}
