using AutoMapper;
using WoodSalesApi.DTOs;
using WoodSalesApi.Models;

namespace WoodSalesApi.Automappers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientInsertDto, Client>();
            CreateMap<SaleInsertDto, Sale>();
            CreateMap<SaleDetailInsertDto, SaleDetail>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDetail, SaleDetailDto>();
            CreateMap<SaleUpdateDto, Sale>();
            CreateMap<SaleDetailUpdateDto, SaleDetail>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductInsertDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
