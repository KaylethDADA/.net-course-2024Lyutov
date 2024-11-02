using BankSystem.Application.Dto.ClientDto;
using FluentValidation;

namespace BankSystem.Application.FluentValidations.ClientValidations
{
    public class UpdateClientRequestValidator : BaseClientRequestValidator<UpdateClientRequest>
    {
        public UpdateClientRequestValidator()
        {
            ApplyCommonRules();

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("Требуется ClientId.");
        }
    }
}
