namespace WoodSalesApi.DTOs
{
	public class SaleDetailUpdateDto
	{
		public long Id { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total { get; set; }
		public int IdProduct { get; set; }
	}
}
