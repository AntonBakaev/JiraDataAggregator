using System;
using System.Text;

namespace Common.Helpers
{
	public class StringExtensions
	{
		public static string ToBase64String(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return Convert.ToBase64String(bytes);
		}
	}
}
