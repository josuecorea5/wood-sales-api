using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class SaleUpdateValidator : AbstractValidator<SaleUpdateDto>
	{
        public SaleUpdateValidator()
        {
            RuleFor(x => x.IdClient).NotEmpty().NotNull().GreaterThan(0).WithMessage("{PropertyName} should not be empty and should be greater than 0");
			RuleForEach(x => x.SaleDetails).SetValidator(new SaleDetailUpdateValidator());
		}
    }
}
