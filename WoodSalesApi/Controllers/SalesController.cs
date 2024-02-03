using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodSalesApi.DTOs;
using WoodSalesApi.Services;

namespace WoodSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SalesController : ControllerBase
	{
		private readonly ISaleService _saleService;
		private readonly IValidator<SaleInsertDto> _saleInsertValidator;
		private readonly IValidator<SaleUpdateDto> _saleUpdateValidator;

		public SalesController(ISaleService saleService, IValidator<SaleInsertDto> saleInsertValidator, IValidator<SaleUpdateDto> saleUpdateValidator)
		{
			_saleService = saleService;
			_saleInsertValidator = saleInsertValidator;
			_saleUpdateValidator = saleUpdateValidator;
		}

		[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
			=> Ok(await _saleService.GetAll());

		//[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(long id)
		{
			var sale = await _saleService.GetById(id);

			if(sale is null)
			{
				return NotFound(_saleService.Errors);
			}

			return Ok(sale);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPost]
		public async Task<IActionResult> Add(SaleInsertDto saleCreateDto)
		{
			var validationResult = _saleInsertValidator.Validate(saleCreateDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(
					validationResult.Errors.Select(e => new { e.ErrorMessage })	
				);
			}

			try
			{
				var sale = await _saleService.Add(saleCreateDto);

				if(sale is null) return NotFound(_saleService.Errors);

				return Ok(sale);
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(long id, SaleUpdateDto saleUpdateDto)
		{

			var validationResult = _saleUpdateValidator.Validate(saleUpdateDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(
					validationResult.Errors.Select(e => new { e.ErrorMessage })
				);
			}

			try
			{
				var updateSale = await _saleService.Update(id, saleUpdateDto);

				if(updateSale is null)
				{
					return NotFound(_saleService.Errors);
				}
				return Ok(updateSale);
			}
			catch (Exception e)
			{
				return BadRequest(new { message = e.Message });
			}
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var saleDeleted = await _saleService.Delete(id);

			if(saleDeleted is null) return NotFound();

			return Ok(saleDeleted);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpDelete("{id}/details/{idDetailSale}")]
		public async Task<IActionResult> DeleteDetail(long id, long idDetailSale)
		{
			try
			{
				var detailSale = await _saleService.DeleteSaleDetail(id, idDetailSale);

				if (detailSale is null) return NotFound();

				return Ok(detailSale);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
