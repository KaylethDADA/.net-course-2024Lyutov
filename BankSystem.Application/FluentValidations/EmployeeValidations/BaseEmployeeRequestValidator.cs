using BankSystem.Application.Dto.EmployeeDto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BankSystem.Application.FluentValidations.EmployeeValidations
{
    public class BaseEmployeeRequestValidator<T> : AbstractValidator<T>
        where T : EmployeeRequest
    {
        protected void ApplyCommonRules()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Номер телефона обязателен для заполнения.")
                .Must(x => Regex.IsMatch(x.Replace(" ", ""), @"^\+37377[4-9][0-9]{5}$"))
                .WithMessage("Некорректный номер телефона!");

            RuleFor(x => x.PassportNumber)
                .NotEmpty()
                .WithMessage("Номер паспорта обязателен для заполнения.")
                .Length(8, 12)
                .WithMessage("Номер паспорта должен содержать от 8 до 12 символов.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Имя обязательно для заполнения.")
                .MaximumLength(50)
                .WithMessage("Имя не должно превышать 50 символов.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Фамилия обязательна для заполнения.")
                .MaximumLength(50)
                .WithMessage("Фамилия не должна превышать 50 символов.");

            RuleFor(x => x.BirthDay)
                .LessThan(DateTime.Today)
                .WithMessage("Дата рождения должна быть в прошлом.")
                .LessThan(DateTime.Now.AddYears(-18))
                .WithMessage("Клиент должен быть не моложе 18 лет.")
                .Must(x => (DateTime.Today - x).TotalDays / 365 <= 150)
                .WithMessage("Возраст клиента должен быть менее 150 лет.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .WithMessage("Заработная плата должна быть больше нуля.");

            RuleFor(x => x.Contract)
                .NotEmpty()
                .WithMessage("Контракт обязателен для заполнения.")
                .MaximumLength(100)
                .WithMessage("Контракт не должен превышать 100 символов.");
        }
    }
}
