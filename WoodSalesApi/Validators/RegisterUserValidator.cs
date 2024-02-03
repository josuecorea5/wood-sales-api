using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
	{
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("{PropertyName} should not be empty").MinimumLength(5).WithMessage("{PropertyName} should have at least 5 characters");

            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("{PropertyName} should not be empty").EmailAddress().WithMessage("{PropertyName} should be valid");

            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("{PropertyName} should contains a non-alphanumeric value, an uppercase, an lowercase and a digit");
		}
    }
}
