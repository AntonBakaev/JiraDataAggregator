namespace Core.ViewModels
{
	public class FlowStatisticsVm
	{
		public int Passed { get; set; }
		public int Failed { get; set; }
		public int Wip { get; set; }
		public int Blocked { get; set; }
		public string Filter { get; set; }
		public bool IsFiltered { get; set; }
	}
}
