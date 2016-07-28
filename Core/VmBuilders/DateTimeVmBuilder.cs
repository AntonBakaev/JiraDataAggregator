using System;
using System.Globalization;
using Core.ViewModels;
using Core.VmBuilders.Interfaces;

namespace Core.VmBuilders
{
	public class DateTimeVmBuilder : IDateTimeVmBuilder
	{
		public DateTimeVm GetDateTimeWhenReportGenerated()
		{
			return new DateTimeVm { Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm", CultureInfo.CurrentCulture) };
		}
	}
}
