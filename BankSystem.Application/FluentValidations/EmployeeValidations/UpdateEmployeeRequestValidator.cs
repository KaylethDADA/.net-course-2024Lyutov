using BankSystem.Application.Dto.EmployeeDto;
using FluentValidation;

namespace BankSystem.Application.FluentValidations.EmployeeValidations
{
    public class UpdateEmployeeRequestValidator : BaseEmployeeRequestValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            ApplyCommonRules();

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("Требуется ClientId.");
        }
    }
}
