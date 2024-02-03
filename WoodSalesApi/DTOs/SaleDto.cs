namespace WoodSalesApi.DTOs
{
	public class SaleDto
	{
		public long Id { get; set; }
		public DateTime DateSale { get; set; }
		public decimal Total { get; set; }
		public ICollection<SaleDetailDto>? SaleDetails { get; set; }

    }
}
