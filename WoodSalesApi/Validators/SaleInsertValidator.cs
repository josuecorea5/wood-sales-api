using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class SaleInsertValidator : AbstractValidator<SaleInsertDto>
	{
        public SaleInsertValidator()
        {
            RuleFor(x => x.IdClient).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(x => x.SaleDetails).NotEmpty().NotNull().WithMessage("{PropertyName} should not be null");
			RuleForEach(x => x.SaleDetails).SetValidator(new SaleDetailInsertValidator());
        }
    }
}
