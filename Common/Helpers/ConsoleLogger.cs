using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers.Interfaces;

namespace Common.Helpers
{
	public class ConsoleLogger: ILogger
	{
		public void Log(Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
