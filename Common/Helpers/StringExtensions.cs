using System;
using System.Text;

namespace Common.Helpers
{
	public class StringExtensions
	{
		public static string ToToBase64String(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return Convert.ToBase64String(bytes);
		}
	}
}
