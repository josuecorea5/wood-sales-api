using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
	{
        public ProductUpdateValidator()
        {
			RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{PropertyName} should not be empty").MinimumLength(4).WithMessage("{PropertyName} should have at least 4 characters");
			RuleFor(x => x.UnitPrice).NotEmpty()
				.GreaterThan(0).WithMessage("{PropertyName} should not be empty and should be greater than 0");
		}
    }
}
