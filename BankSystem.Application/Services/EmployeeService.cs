using AutoMapper;
using BankSystem.Application.Dto.EmployeeDto;
using BankSystem.Application.Exceptions;
using BankSystem.Application.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Services
{
    public class EmployeeService 
    {
        private readonly IStorage<Employee> _employeeStorage;
        private readonly IMapper _mapper;

        public EmployeeService(IStorage<Employee> employeeStorage, IMapper mapper)
        {
            _employeeStorage = employeeStorage;
            _mapper = mapper;
        }

        public async Task AddEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = _mapper.Map<Employee>(request);
                await _employeeStorage.AddAsync(employee, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while adding the {nameof(Employee)}.", ex);
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = _mapper.Map<Employee>(request);
                await _employeeStorage.UpdateAsync(employee, cancellationToken);
                return _mapper.Map<EmployeeResponse>(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while updating the {nameof(Employee)}.", ex);
            }
        }

        public async Task<ICollection<EmployeeResponse>> GetAsync(GetEmployeeFilterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var filter = _mapper.Map<Expression<Func<Employee, bool>>>(request);
                var employees = await _employeeStorage.GetAsync(filter, request.PageNumber ?? null, request.PageSize ?? null, cancellationToken);
                return _mapper.Map<ICollection<EmployeeResponse>>(employees);
            }
            catch (Exception ex)
            {
                throw new EmployeeException($"An error occurred while retrieving {nameof(Employee)}.", ex);
            }
        }

        public async Task<EmployeeResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeStorage.GetByIdAsync(id, cancellationToken);
                return _mapper.Map<EmployeeResponse>(employee);
            }
            catch (Exception ex)
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
