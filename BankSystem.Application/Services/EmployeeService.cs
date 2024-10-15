using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

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
                throw new EmployeeValidationException($"The {nameof(Employee)} cannot be null.");

            if (employee.BirthDay > DateTime.Now.AddYears(-18))
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
                throw new EmployeeValidationException($"The {nameof(Employee)} cannot be null.");

            if (employee.BirthDay > DateTime.Now.AddYears(-18))
                throw new EmployeeValidationException($"{nameof(Employee)} must be over 18 years old.");

            if (string.IsNullOrWhiteSpace(employee.PassportNumber))
                throw new EmployeeValidationException($"The {nameof(Employee)} must have passport details.");

            try
            {
                _employeeStorage.Update(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while updating the {nameof(Employee)}.", ex);
            }
        }

        public ICollection<Employee> Get(Expression<Func<Employee, bool>> filter, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new EmployeeException("Page number must be greater than zero.");

            if (pageSize <= 0)
                throw new EmployeeException("Page size must be greater than zero.");

            try
            {
                return _employeeStorage.Get(filter, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while retrieving {nameof(Employee)}.", ex);
            }
        }

        public Employee GetById(Guid id)
        {
            try
            {
                return _employeeStorage.GetById(id);
            }
            catch(Exception ex)
            {
                throw new EmployeeException($"An error occurred while retrieving {nameof(Employee)}.", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _employeeStorage.Delete(id);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while deleting {nameof(Employee)}.", ex);
            }
        }
    }
}
