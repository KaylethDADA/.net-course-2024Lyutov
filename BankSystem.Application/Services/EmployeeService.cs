using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeService(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void Add(Employee employee)
        {
            if (employee == null)
                throw new EmployeeValidationException($"The old or new {nameof(Employee)} cannot be zero.");

            if (employee.Age < 18)
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            try
            {
                _employeeStorage.Add(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while adding the {nameof(Employee)}.", ex);
            }
        }

        public void Update(Employee employee)
        {
            if (employee == null)
                throw new EmployeeValidationException($"The old or new {nameof(Employee)} cannot be zero.");

            if (employee.Age < 18)
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            try
            {
                _employeeStorage.Update(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while editing the {nameof(Employee)}.", ex);
            }
        }

        public List<Employee> Get(Func<Employee, bool>? filter)
        {
            return _employeeStorage.Get(filter);
        }

        public void Delete(Employee employee)
        {
            try
            {
                _employeeStorage.Delete(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Employee)}.", ex);
            }
        }
    }
}
