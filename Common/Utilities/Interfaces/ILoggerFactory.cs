using System;
using NLog;

namespace Common.Utilities.Interfaces
{
	public interface ILoggerFactory
	{
		ILogger Create(Type type);
	}
}
