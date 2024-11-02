using BankSystem.Application.Dto.ClientDto;

namespace BankSystem.Application.FluentValidations.ClientValidations
{
    public class CreateClientRequestValidator : BaseClientRequestValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            ApplyCommonRules();
        }
    }
}
