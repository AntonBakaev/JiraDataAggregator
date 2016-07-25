namespace Core.ViewModels
{
	public class RetailShopFlowStatisticsVm
	{
		public int Passed { get; set; }
		public int Failed { get; set; }
		public int Wip { get; set; }
		public int Blocked { get; set; }
	}
}
