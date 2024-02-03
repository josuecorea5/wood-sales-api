using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WoodSalesApi.DTOs;
using WoodSalesApi.Services;

namespace WoodSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto> _productService;
		private readonly IValidator<ProductInsertDto> _productInsertValidator;
		private readonly IValidator<ProductUpdateDto> _productUpdateValidator;
		public ProductsController(ICommonService<ProductDto, ProductInsertDto, ProductUpdateDto> productService, IValidator<ProductInsertDto> productInsertValidator, IValidator<ProductUpdateDto> productUpdateValidator)
		{
			_productService = productService;
			_productInsertValidator = productInsertValidator;
			_productUpdateValidator = productUpdateValidator;
		}

		[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _productService.GetAll());
		}

		[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productService.GetById(id);

			if (product == null) return BadRequest(_productService.Errors);
			
			return Ok(product);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPost]
		public async Task<IActionResult> Add(ProductInsertDto productInsertDto)
		{
			var validationResult = _productInsertValidator.Validate(productInsertDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			var product = await _productService.Add(productInsertDto);

			return Ok(product);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, ProductUpdateDto productUpdateDto)
		{

			var validationResult = _productUpdateValidator.Validate(productUpdateDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			var product = await _productService.Update(id, productUpdateDto);

			if(product is null) return NotFound(_productService.Errors);

			return Ok(product);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productService.Delete(id);

			if (product is null) return NotFound(_productService.Errors);

			return Ok(product);
		}
	}
}
