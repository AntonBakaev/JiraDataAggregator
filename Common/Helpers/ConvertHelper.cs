using System.Collections.Generic;
using System.Linq;

namespace Common.Helpers
{
	public class ConvertHelper
	{
		public static Dictionary<string, string> ToDictionary(object data)
		{
			if (data == null)
				return null;

			return data.GetType().GetProperties()
				.ToDictionary(x => x.Name, x => x.GetGetMethod().Invoke(data, null) == null
					? ""
					: x.GetGetMethod().Invoke(data, null).ToString());
		}
	}
}
