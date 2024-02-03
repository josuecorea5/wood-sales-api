using AutoMapper;
using WoodSalesApi.DTOs;
using WoodSalesApi.Models;
using WoodSalesApi.Repositories;

namespace WoodSalesApi.Services
{
	public class ProductService : ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto>
	{
		private readonly IRepository<Product> _productRepository;
		private readonly IMapper _mapper;
		public List<string> Errors { get; }

		public ProductService(IRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
			Errors = new List<string>();
		}
		public async Task<IEnumerable<ProductDto>> GetAll()
		{
			var products = await _productRepository.GetAll();
			return products.Select(x => _mapper.Map<ProductDto>(x));
		}
		public async Task<ProductDto> GetById<I>(I id)
		{
			var product = await _productRepository.GetById(id);

			if (product is null)
			{
				Errors.Add($"Product with id {id} not found");
				return null;
			};

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> Add(ProductInsertDto product)
		{
			var newProduct = _mapper.Map<Product>(product);
			await _productRepository.Add(newProduct);
			await _productRepository.Save();
			return _mapper.Map<ProductDto>(newProduct);
		}
		public async Task<ProductDto> Update<I>(I id, ProductUpdateDto productUpdateDto)
		{
			var product = await _productRepository.GetById(id);

			if (product is null)
			{
				Errors.Add($"Product with id {id} not found");
				return null;
			};

			_mapper.Map(productUpdateDto, product);

			_productRepository.Update(product);
			await _productRepository.Save();

			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> Delete<I>(I id)
		{
			var product = await _productRepository.GetById(id);

			if (product is null)
			{
				Errors.Add($"Product with id {id} not found");
				return null;
			};

			_productRepository.Delete(product);
			await _productRepository.Save();

			return _mapper.Map<ProductDto>(product);
		}
	}
}
