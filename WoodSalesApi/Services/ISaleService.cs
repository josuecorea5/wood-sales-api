using WoodSalesApi.DTOs;

namespace WoodSalesApi.Services
{
	public interface ISaleService : ICommonService<SaleDto, SaleInsertDto, SaleUpdateDto>
	{
		Task<SaleDetailDto> DeleteSaleDetail(long saleId, long saleDetailId);
	}
}
