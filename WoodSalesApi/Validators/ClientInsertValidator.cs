using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class ClientInsertValidator : AbstractValidator<ClientInsertDto>
	{
		public ClientInsertValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{PropertyName} should not be empty").MinimumLength(5).WithMessage("{PropertyName} should have at least 5 characters");
		}
	}
}
