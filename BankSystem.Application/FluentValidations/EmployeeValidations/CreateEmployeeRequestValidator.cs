using BankSystem.Application.Dto.EmployeeDto;

namespace BankSystem.Application.FluentValidations.EmployeeValidations
{
    public class CreateEmployeeRequestValidator : BaseEmployeeRequestValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            ApplyCommonRules();
        }
    }
}
