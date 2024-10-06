using BankSystem.Application.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Application.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _employeeStorage;

        public EmployeeService(EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.Age < 18)
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            var existingEmployee = _employeeStorage.GetAllEmployees()
                .FirstOrDefault(c => c.PassportNumber == employee.PassportNumber);

            if (existingEmployee != null)
                throw new EmployeeValidationException($"A {nameof(Employee)} with the same passport number already exists.");

            try
            {
                _employeeStorage.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while adding the {nameof(Employee)}.", ex);
            }
        }

        public void UpdateEmployee(Employee oldEmployee, Employee newEmployee)
        {
            if (oldEmployee == null || newEmployee == null)
                throw new EmployeeValidationException($"The old or new {nameof(Employee)} cannot be zero.");

            if (newEmployee.Age < 18)
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(newEmployee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            if(!oldEmployee.PassportNumber.Equals(newEmployee.PassportNumber))
                throw new EmployeeValidationException($"The new passport number must match the existing passport number for this {nameof(Employee)}.");

            try
            {
                _employeeStorage.UpdateEmployee(oldEmployee, newEmployee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while editing the {nameof(Employee)}.", ex);
            }
        }

        public List<Employee> GetEmployeeByFilter(
            string? fullName,
            string? phoneNumber,
            string? passportNumber,
            DateTime? birthDateTo,
            DateTime? birthDateFrom)
        {
            return _employeeStorage.GetEmployeeByFilter(fullName, phoneNumber, passportNumber, birthDateTo, birthDateFrom);
        }
    }
}
