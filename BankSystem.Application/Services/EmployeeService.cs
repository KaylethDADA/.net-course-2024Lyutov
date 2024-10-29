using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Services
{
    public class EmployeeService 
    {
        private readonly IStorage<Employee> _employeeStorage;

        public EmployeeService(IStorage<Employee> employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public async Task AddEmploeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            if (employee == null)
                throw new EmployeeValidationException($"The {nameof(Employee)} cannot be null.");

            if (employee.BirthDay > DateTime.Now.AddYears(-18))
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            try
            {
                await _employeeStorage.AddAsync(employee, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while adding the {nameof(Employee)}.", ex);
            }
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            if (employee == null)
                throw new EmployeeValidationException($"The {nameof(Employee)} cannot be null.");

            if (employee.BirthDay > DateTime.Now.AddYears(-18))
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            try
            {
                await _employeeStorage.UpdateAsync(employee, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while updating the {nameof(Employee)}.", ex);
            }
        }

        public async Task<ICollection<Employee>> GetAsync(
            Expression<Func<Employee, bool>>? filter,
            int? pageNumber,
            int? pageSize,
            CancellationToken cancellationToken)
        {
            if (pageNumber <= 0)
                throw new EmployeeException("Page number must be greater than zero.");

            if (pageSize <= 0)
                throw new EmployeeException("Page size must be greater than zero.");

            try
            {
                return await _employeeStorage.GetAsync(filter, pageNumber, pageSize, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while retrieving {nameof(Employee)}.", ex);
            }
        }

        public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _employeeStorage.GetByIdAsync(id, cancellationToken);
            }
            catch(Exception ex)
            {
                throw new EmployeeException($"An error occurred while retrieving {nameof(Employee)}.", ex);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _employeeStorage.DeleteAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Employee)}.", ex);
            }
        }
    }
}
