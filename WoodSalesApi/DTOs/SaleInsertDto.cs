namespace WoodSalesApi.DTOs
{
	public class SaleInsertDto
	{
		public int IdClient {  get; set; }
        public List<SaleDetailInsertDto> SaleDetails  { get; set; }
	}
}
