using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class SaleDetailUpdateValidator : AbstractValidator<SaleDetailUpdateDto>
	{
		public SaleDetailUpdateValidator()
		{
			RuleFor(d => d.Quantity).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} should not be empty and should be greater than 0");
			RuleFor(d => d.UnitPrice).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} should not be empty and should be greater than 0");
			RuleFor(d => d.Total).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} should not be empty and should be greater than 0");
			RuleFor(d => d.IdProduct).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} is required and should be greater than 0");
		}
	}
}
