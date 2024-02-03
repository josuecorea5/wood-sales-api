using FluentValidation;
using WoodSalesApi.DTOs;

namespace WoodSalesApi.Validators
{
	public class ClientUpdateValidator : AbstractValidator<ClientUpdateDto>
	{
		public ClientUpdateValidator()
		{
			Include(new ClientInsertValidator());
		}
	}
}
