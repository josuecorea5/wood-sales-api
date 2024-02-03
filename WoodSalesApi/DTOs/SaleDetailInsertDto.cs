namespace WoodSalesApi.DTOs
{
	public class SaleDetailInsertDto
	{
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total { get; set; }
		public int IdProduct { get; set; }
	}
}
