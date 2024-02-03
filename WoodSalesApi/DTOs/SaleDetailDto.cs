namespace WoodSalesApi.DTOs
{
	public class SaleDetailDto
	{
        public long Id { get; set; }
		public long IdSale { get; set; }
        public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total {  get; set; }
    }
}
