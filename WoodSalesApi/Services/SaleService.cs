using AutoMapper;
using WoodSalesApi.DTOs;
using WoodSalesApi.Models;
using WoodSalesApi.Repositories;

namespace WoodSalesApi.Services
{
	public class SaleService : ISaleService
	{
		private readonly ITransactionalRepository<Sale> _saleRepository;
		private readonly IRepository<SaleDetail> _saleDetailRepository;
		private readonly ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto> _clientService;
		private readonly ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto> _productService;
		private readonly IMapper _mapper;
		public List<string> Errors { get; }

		public SaleService(ITransactionalRepository<Sale> saleRepository, IRepository<SaleDetail> saleDetailRepository, IMapper mapper, ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto> clientService, ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto> productService)
		{
			_saleRepository = saleRepository;
			_saleDetailRepository = saleDetailRepository;
			_mapper = mapper;
			_clientService = clientService;
			_productService = productService;
			Errors = new List<string>();
		}

		public async Task<IEnumerable<SaleDto>> GetAll()
		{
			var sales = await _saleRepository.GetAll();

			return sales.Select(s => _mapper.Map<SaleDto>(s));
		}
		public async Task<SaleDto> GetById<I>(I id)
		{
			var sale = await _saleRepository.GetById(id);

			if (sale is null)
			{
				Errors.Add($"sale with id {id} does not exist");
				return null;
			}

			return _mapper.Map<SaleDto>(sale);
		}

		public async Task<SaleDto> Add(SaleInsertDto saleCreateDto)
		{
			var existClient = await _clientService.GetById(saleCreateDto.IdClient);

			if(existClient is null)
			{
				Errors.Add($"client with id {saleCreateDto.IdClient} does not exist");
				return null;
			}

			var sale = _mapper.Map<Sale>(saleCreateDto);
			sale.DateSale = DateTime.Now;
			sale.Total = saleCreateDto.SaleDetails.Sum(x => x.Quantity * x.UnitPrice);

			using var transaction = _saleRepository.BeginTransaction();

			try
			{
				await _saleRepository.Add(sale);
				foreach (var saleDetail in saleCreateDto.SaleDetails)
				{
					var existProduct = await _productService.GetById(saleDetail.IdProduct);

					if (existProduct is null)
					{
						Errors.Add($"product with id {saleDetail.IdProduct} does not exist");
						return null;
					}

					var detailSale = _mapper.Map<SaleDetail>(saleDetail);
					detailSale.IdSale = sale.Id;
				}
				await _saleRepository.Save();

				transaction.Commit();
			}catch (Exception)
			{
				transaction.Rollback();
				throw new Exception("Something failed while creating the sale");
			}

			var newSale = await _saleRepository.GetById<long>(sale.Id);

			return _mapper.Map<SaleDto>(newSale);
		}

		public async Task<SaleDto> Delete<I>(I id)
		{
			var sale = await _saleRepository.GetById(id);

			if (sale is null)
			{
				Errors.Add($"sale with id {id} does not exist");
				return null;
			}

			var saleDeletedDto = _mapper.Map<SaleDto>(sale);

			_saleRepository.Delete(sale);
			await _saleRepository.Save();

			return saleDeletedDto;
		}

		public async Task<SaleDetailDto> DeleteSaleDetail(long saleId, long saleDetailId)
		{
			var sale = await _saleRepository.GetById(saleId);

			if (sale is null)
			{
				Errors.Add($"sale with id {saleId} does not exist");
				return null;
			}

			var detailSale = await _saleDetailRepository.GetById(saleDetailId);

			if (detailSale is null)
			{
				Errors.Add($"sale detail with id {saleDetailId} does not exist");
				return null;
			};

			var detailSaleDto = _mapper.Map<SaleDetailDto>(detailSale);

			using var transaction = _saleRepository.BeginTransaction();

			try
			{
				_saleDetailRepository.Delete(detailSale);
				await _saleDetailRepository.Save();

				if (sale.SaleDetails.Count == 0)
				{
					sale.Total = 0;
				}
				else
				{
					sale.Total = sale.SaleDetails.Sum(ds => ds.Quantity * ds.UnitPrice);
				}
				await _saleRepository.Save();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw new Exception("Something failed while deleted detail sale");
			}

			return detailSaleDto;
		}

		public async Task<SaleDto> Update<I>(I id, SaleUpdateDto saleUpdateDto)
		{
			var existClient = await _clientService.GetById(saleUpdateDto.IdClient);

			if (existClient is null)
			{
				Errors.Add($"client with id {saleUpdateDto.IdClient} does not exist");
			}

			var sale = await _saleRepository.GetById(id);

			if (sale is null)
			{
				Errors.Add($"Sale with id {id} does not exist");
				return null;
			};

			using var transaction = _saleRepository.BeginTransaction();

			try
			{
				foreach (var saleItem in saleUpdateDto.SaleDetails)
				{
					var existProduct = await _productService.GetById(saleItem.IdProduct);

					if (existProduct is null)
					{
						Errors.Add($"product with id {saleItem.IdProduct} does not exist");
						return null;
					}

					var existSaleDetail = await _saleDetailRepository.GetById(saleItem.Id);

					if (existSaleDetail != null)
					{
						_mapper.Map(saleItem, existSaleDetail);
						_saleDetailRepository.Update(existSaleDetail);
					}
					else
					{
						var newDetailSale = _mapper.Map<SaleDetail>(saleItem);
						newDetailSale.IdSale = sale.Id;
						await _saleDetailRepository.Add(newDetailSale);
					}
				}
				await _saleDetailRepository.Save();
				sale.Total = sale.SaleDetails.Sum(ds => ds.Quantity * ds.UnitPrice);
				sale.IdClient = saleUpdateDto.IdClient;
				await _saleRepository.Save();
				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw new Exception("Something failed while updating the sale");
			}
			return _mapper.Map<SaleDto>(sale);
		}
	}
}
