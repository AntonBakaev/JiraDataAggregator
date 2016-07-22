using Common.Enums;

namespace Core.Models
{
	public class TestStep
	{
		public string StepId { get; set; }
		public int OrderId { get; set; }
		public string Step { get; set; }
		public string Data { get; set; }
		public string Result { get; set; }
		public StepStatus StepStatus { get; set; }
		public string StepComment { get; set; }
	}
}
