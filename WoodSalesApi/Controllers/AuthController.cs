using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WoodSalesApi.DTOs;
using WoodSalesApi.Services;

namespace WoodSalesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IValidator<LoginUserDto> _loginValidator;
		private readonly IValidator<RegisterUserDto> _registerUserValidator;

		public AuthController(IAuthService authService, IValidator<LoginUserDto> loginValidator, IValidator<RegisterUserDto> registerUserValidator)
		{
			_authService = authService;
			_loginValidator = loginValidator;
			_registerUserValidator = registerUserValidator;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
		{
			var validationResult = _registerUserValidator.Validate(registerUserDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			if(await _authService.Register(registerUserDto))
			{
				return Ok("User register successfully"); 
			}

			return BadRequest(_authService.Errors);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginUserDto loginUserDto)
		{
			var validationResult = _loginValidator.Validate(loginUserDto);

			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.ErrorMessage }));
			}

			var userToken = await _authService.Login(loginUserDto);

			if(userToken is not null)
			{
				return Ok(new { token = userToken });
			}

			return BadRequest(_authService.Errors);
		}
	}
}
