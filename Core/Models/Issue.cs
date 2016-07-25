using System.Collections.Generic;

namespace Core.Models
{
	public class Issue
	{
		public string Key { get; set; }
		public List<string> ExecutedDeffectsKeys { get; set; }
	}
}
