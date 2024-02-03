using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodSalesApi.DTOs;
using WoodSalesApi.Models;
using WoodSalesApi.Services;

namespace WoodSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
		private readonly ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto> _clientService;
		private readonly IValidator<ClientInsertDto> _clientInsertValidator;
		private readonly IValidator<ClientUpdateDto> _clientUpdateValidator;

		public ClientsController(ICommonService<ClientDto, ClientInsertDto, ClientUpdateDto> clientService, IValidator<ClientInsertDto> clientValidator, IValidator<ClientUpdateDto> clientUpdateValidator)
		{
			_clientService = clientService;
			_clientInsertValidator = clientValidator;
			_clientUpdateValidator = clientUpdateValidator;
		}

		[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet]
		public async Task<IEnumerable<ClientDto>> Get() 
			=> await _clientService.GetAll();

		[Authorize(Policy = "AdminUserRolePolicy")]
		[HttpGet("{id}")]
		public async Task<ActionResult<ClientDto>> GetById(int id)
		{

			var client = await _clientService.GetById(id);

			if (client is null)
			{
				return NotFound(_clientService.Errors);
			}

			return Ok(client);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPost]
		public async Task<ActionResult<ClientDto>> Add(ClientInsertDto clientInsertDto)
		{
			var validationResult = _clientInsertValidator.Validate(clientInsertDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			return await _clientService.Add(clientInsertDto);
		}

		[Authorize(Policy = "AdminRolePolicy")]
		[HttpPut("{id}")]
		public async Task<ActionResult<ClientDto>> Update(int id, ClientUpdateDto clientUpdateDto)
		{
			var validationResult = _clientUpdateValidator.Validate(clientUpdateDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			var client = await _clientService.Update(id, clientUpdateDto);

			if (client is null)
			{
				return NotFound();
			}

			return Ok(client);
		}
			
		[Authorize(Policy = "AdminRolePolicy")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<ClientDto>> Delete(int id)
		{
			var client = await _clientService.Delete(id);

			if(client is null)
			{
				return NotFound();
			}

			return Ok(client);
		}
	}
}
