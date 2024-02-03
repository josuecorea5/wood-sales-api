using WoodSalesApi.Models;

namespace WoodSalesApi.DTOs
{
	public class SaleUpdateDto
	{
		public int IdClient { get; set; }
		public List<SaleDetailUpdateDto> SaleDetails { get; set; } = [];
	}
}
