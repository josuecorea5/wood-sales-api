using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class LoginUserValidator : AbstractValidator<LoginUserDto>
	{
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("{PropertyName} should not be empty").EmailAddress().WithMessage("{PropertyName} should be valid");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("{ProperyName} should not be empty").MinimumLength(8).WithMessage("{PropertyName} should be at least 8 characters");
        }
    }
}
